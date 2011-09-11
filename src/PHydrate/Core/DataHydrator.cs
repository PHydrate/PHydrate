#region Copyright

// This file is part of PHydrate.
// 
// PHydrate is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// PHydrate is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with PHydrate.  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright 2010-2011, Stephen Michael Czetty

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PHydrate.Attributes;
using PHydrate.Util;

namespace PHydrate.Core
{
    public partial class Session
    {
        private class DataHydrator< T > //where T : class
        {
            private readonly IDefaultObjectHydrator _defaultObjectHydrator;
            private readonly WeakReferenceObjectCache _hydratedObjects;
            private readonly SessionFactory _sessionFactory;

            public DataHydrator( IDefaultObjectHydrator defaultObjectHydrator, WeakReferenceObjectCache hydratedObjects, SessionFactory sessionFactory )
            {
                _defaultObjectHydrator = defaultObjectHydrator;
                _hydratedObjects = hydratedObjects;
                _sessionFactory = sessionFactory;
            }

            public IEnumerable< T > HydrateFromDataReader( IDataReader dataReader )
            {
                IEnumerable< IMemberInfo > internalRecordsets =
                    typeof(T).GetMembersWithAttribute< RecordsetAttribute >().ToList();
                return internalRecordsets.Any()
                           ? HydrateRecordsetWithInternals( dataReader, internalRecordsets )
                           : HydrateRecordset( dataReader );
            }

            /// <exception cref="PHydrateException">Missing expected recordset from stored procedure</exception>
            [ NotNull ]
            private IEnumerable< T > HydrateRecordsetWithInternals( IDataReader dataReader,
                                                                    IEnumerable< IMemberInfo > internalRecordsets )
            {
                IDictionary< int, T > aggregateRoot =
                    HydrateRecordset( dataReader ).ToDictionary( x => x.GetObjectsHashCodeByPrimaryKeys() );

                foreach ( IMemberInfo internalRecordset in internalRecordsets )
                {
                    if ( !dataReader.NextResult() )
                        throw new PHydrateException( "Missing expected recordset from stored procedure" );

                    Type typeToCastTo = GetTypeToCastTo( internalRecordset );

                    IEnumerable enumerable =
                        typeToCastTo.ExecuteGenericMethod< DataHydrator< T >, IEnumerable >(
                            x => x.HydrateFromDataReader( dataReader ),
                            _defaultObjectHydrator,
                            _hydratedObjects,
                            _sessionFactory
                            );

                    Type dictionaryKeyType;
                    if ( InternalIsEnumerable( typeToCastTo, internalRecordset.Type ) )
                        // IEnumerable, IList
                        HydrateInnerRecordsetEnumerable( internalRecordset, typeToCastTo, enumerable, aggregateRoot );
                    else if ( InternalIsDictionary( typeToCastTo, internalRecordset.Type, out dictionaryKeyType ) )
                        // Dictionary
                        HydrateRecordsetDictionary( internalRecordset, typeToCastTo, enumerable, aggregateRoot,
                                                    dictionaryKeyType );
                    else if (InternalIsSimpleType( typeToCastTo, internalRecordset.Type ) ) // Simple type
                        SetSimpleTypeInAggregateRoot( internalRecordset,
                                                      enumerable.Cast< object >().FirstOrDefault(),
                                                      aggregateRoot );
                    else
                        throw new PHydrateException(
                            "Unable to map internal recordset.  Check the types to make sure they match!" );

                }
                return aggregateRoot.Values;
            }

            private static bool InternalIsEnumerable(Type typeToCastTo, Type internalType)
            {
                return typeof(IEnumerable< >).MakeGenericType( typeToCastTo ).IsAssignableFrom( internalType );
            }

            private static bool InternalIsDictionary(Type typeToCastTo, Type internalType, out Type dictionaryKeyType)
            {
                var primaryKeys = typeToCastTo.GetMembersWithAttribute< PrimaryKeyAttribute >().ToList();
                dictionaryKeyType = primaryKeys.Count == 1 ? primaryKeys[ 0 ].Type : null;
                return dictionaryKeyType != null &&
                       typeof(IDictionary< , >).MakeGenericType( dictionaryKeyType, typeToCastTo ).IsAssignableFrom(
                           internalType );
            }

            private static bool InternalIsSimpleType(Type typeToCastTo, Type internalType)
            {
                return typeToCastTo.IsAssignableFrom( internalType );
            }

            private static void HydrateRecordsetDictionary( IMemberInfo internalRecordset, Type typeToCastTo, IEnumerable enumerable, IDictionary< int, T > aggregateRoot, Type primaryKeyType )
            {
                foreach ( object obj in enumerable )
                {
                    T found = GetAggregateRootFromSecondaryObject( obj, aggregateRoot );
// ReSharper disable CompareNonConstrainedGenericWithNull
                    if ( typeof(T).IsClass && found == null )
                        continue;
// ReSharper restore CompareNonConstrainedGenericWithNull

                    var dictionary = internalRecordset.GetValue( found ) as IDictionary;
                    if ( dictionary == null )
                    {
                        dictionary =
                            typeof(Dictionary< , >).MakeGenericType( primaryKeyType, typeToCastTo ).
                                ConstructUsingDefaultConstructor< IDictionary >();
                        internalRecordset.SetValue( found, dictionary );
                    }
                    dictionary.Add( obj.GetPropertyValuesWithAttribute< PrimaryKeyAttribute >().First(), obj );
                }
            }

            private static void HydrateInnerRecordsetEnumerable( IMemberInfo internalRecordset, Type typeToCastTo, IEnumerable enumerable, IDictionary< int, T > aggregateRoot )
            {
                foreach ( object obj in enumerable )
                {
                    T found = GetAggregateRootFromSecondaryObject( obj, aggregateRoot );
// ReSharper disable CompareNonConstrainedGenericWithNull
                    if ( typeof(T).IsClass && found == null )
                        continue;
// ReSharper restore CompareNonConstrainedGenericWithNull

                    var list = internalRecordset.GetValue( found ) as IList;
                    if ( list == null )
                    {
                        list = typeof(List< >).MakeGenericType( typeToCastTo ).
                            ConstructUsingDefaultConstructor< IList >();
                        internalRecordset.SetValue( found,
                                                    list );
                    }
                    list.Add( obj );
                }
            }

            private static Type GetTypeToCastTo( IMemberInfo internalRecordset )
            {
                Type typeToCastTo = internalRecordset.Type;
                if ( typeof(IEnumerable).IsAssignableFrom( internalRecordset.Type ) )
                    typeToCastTo = internalRecordset.Type.IsGenericType
                                       ? internalRecordset.Type.GetGenericArguments()[
                                           internalRecordset.Type.GetGenericArguments().Length - 1 ]
                                       : typeof(object);
                return typeToCastTo;
            }

            private static T GetAggregateRootFromSecondaryObject( object obj,
                                                                  IDictionary< int, T > aggregateRoot )
            {
                string[] primaryKeyMembers =
                    typeof(T).GetMembersWithAttribute< PrimaryKeyAttribute >().Select(
                        x => x.Wrapped.Name ).ToArray();

                int lookupHash = obj.GetLookupHash< T >( primaryKeyMembers );

                return aggregateRoot.ContainsKey( lookupHash ) ? aggregateRoot[ lookupHash ] : default(T);
            }

            private static void SetSimpleTypeInAggregateRoot( IMemberInfo internalRecordset, object obj,
                                                              IDictionary< int, T > aggregateRoot )
            {
                int objectHash = obj.GetObjectsHashCodeByPrimaryKeys();

                // Find the member(s) in aggregateRoot that contains this member
                foreach (
                    T o in
                        aggregateRoot.Values.AsEnumerable().Where(
                            x =>
                            internalRecordset.GetValue( x ) != null &&
                            internalRecordset.GetValue( x ).GetObjectsHashCodeByPrimaryKeys() == objectHash ) )
                    internalRecordset.SetValue( o, obj );
            }

            private IEnumerable< T > HydrateRecordset( IDataReader dataReader )
            {
                Func< IDictionary< string, object >, T > hydratorFunction = GetHydratorFunction();

                while ( dataReader.Read() )
                {
                    T hydratedObject = hydratorFunction( dataReader.ToDictionary() );
                    yield return GetHydratedObjectFromCache( hydratedObject );
                }
            }

            [ NotNull ]
            private Func< IDictionary< string, object >, T > GetHydratorFunction()
            {
                IObjectHydrator< T > hydrator = GetHydrator<T>();

                return hydrator == null
                           ? (Func< IDictionary< string, object >, T >)
                             ( x => _defaultObjectHydrator.Hydrate< T >( x ) )
                           : ( hydrator.Hydrate );
            }

            private T GetHydratedObjectFromCache( T hydratedObject )
            {
                if ( _hydratedObjects.Contains( hydratedObject ) )
                    return
                        (T)( _hydratedObjects[ hydratedObject ].Target ??
                          ( _hydratedObjects[ hydratedObject ].Target = hydratedObject ) );

                _hydratedObjects.Add( hydratedObject );
                return hydratedObject;
            }
        }
    }
}