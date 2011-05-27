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
        private class DataHydrator<T> where T : class
        {
            private readonly IDefaultObjectHydrator _defaultObjectHydrator;
            private readonly WeakReferenceObjectCache _hydratedObjects;

            public DataHydrator(IDefaultObjectHydrator defaultObjectHydrator, WeakReferenceObjectCache hydratedObjects)
            {
                _defaultObjectHydrator = defaultObjectHydrator;
                _hydratedObjects = hydratedObjects;
            }

            public IEnumerable<T> HydrateFromDataReader(IDataReader dataReader)
            {
                IEnumerable<IMemberInfo> internalRecordsets =
                    typeof(T).GetMembersWithAttribute<RecordsetAttribute>();
                return internalRecordsets.Any()
                           ? HydrateRecordsetWithInternals(dataReader, internalRecordsets)
                           : HydrateRecordset(dataReader);
            }

            /// <exception cref="PHydrateException">Missing expected recordset from stored procedure</exception>
            [NotNull]
            private IEnumerable<T> HydrateRecordsetWithInternals(IDataReader dataReader,
                                                                    IEnumerable<IMemberInfo> internalRecordsets)
            {
                IDictionary<int, T> aggregateRoot =
                    HydrateRecordset(dataReader).ToDictionary(x => x.GetObjectsHashCodeByPrimaryKeys());

                foreach (IMemberInfo internalRecordset in internalRecordsets)
                {
                    if ( !dataReader.NextResult() )
                        throw new PHydrateException( "Missing expected recordset from stored procedure" );

                    Type typeToCastTo = GetTypeToCastTo( internalRecordset );

                    IEnumerable enumerable =
                        typeToCastTo.ExecuteGenericMethod< DataHydrator< T >, IEnumerable >(
                            x => x.HydrateFromDataReader( dataReader ),
                            _defaultObjectHydrator,
                            _hydratedObjects
                            );

                    if ( typeof(IEnumerable< >).MakeGenericType( typeToCastTo ).IsAssignableFrom( internalRecordset.Type ) )
                        // IEnumerable, IList
                    {
                        //var list = internalRecordset.GetEnumerableOrList( enumerable, typeToCastTo );

                        foreach ( object obj in enumerable )
                        {
                            T found = GetAggregateRootFromSecondaryObject( obj, aggregateRoot );
                            if ( found == null )
                                continue;

                            var list = internalRecordset.GetValue( found ) as IList;
                            if (list == null)
                            {
                                list = typeof(List< >).MakeGenericType( typeToCastTo ).
                                    ConstructUsingDefaultConstructor<IList>();
                                internalRecordset.SetValue( found,
                                                            list );
                            }
                            list.Add( obj );
                        }
                    }
                    else
                    {
                        // Look up the primary keys for the internal type
                        var primaryKeys = typeToCastTo.GetMembersWithAttribute< PrimaryKeyAttribute >().ToList();
                        if (primaryKeys.Count == 1 && typeof(IDictionary< , >).MakeGenericType( primaryKeys[0].Type, typeToCastTo ).IsAssignableFrom(internalRecordset.Type ) ) // Dictionary
                        {
                            foreach ( object obj in enumerable )
                            {
                                T found = GetAggregateRootFromSecondaryObject( obj, aggregateRoot );
                                if ( found == null )
                                    continue;

                                var dictionary = internalRecordset.GetValue( found ) as IDictionary;
                                if ( dictionary == null )
                                {
                                    dictionary =
                                        typeof(Dictionary< , >).MakeGenericType( primaryKeys[0].Type, typeToCastTo ).
                                            ConstructUsingDefaultConstructor< IDictionary >();
                                    internalRecordset.SetValue( found, dictionary );
                                }
                                dictionary.Add( obj.GetPropertyValuesWithAttribute< PrimaryKeyAttribute >().First(), obj );
                            }
                        }
                        else if ( typeToCastTo.IsAssignableFrom( internalRecordset.Type ) ) // Simple type
                            SetSimpleTypeInAggregateRoot( internalRecordset,
                                                          enumerable.Cast< object >().FirstOrDefault(),
                                                          aggregateRoot );
                    }
                }
                return aggregateRoot.Values;
            }

            private static Type GetTypeToCastTo(IMemberInfo internalRecordset)
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
                                                                  IDictionary<int, T> aggregateRoot)
            {
                string[] primaryKeyMembers =
                    typeof(T).GetMembersWithAttribute< PrimaryKeyAttribute >().Select(
                        x => x.Wrapped.Name ).ToArray();

                int lookupHash = obj.GetLookupHash< T >( primaryKeyMembers );

                return aggregateRoot.ContainsKey(lookupHash) ? aggregateRoot[lookupHash] : null;
            }

            private static void SetSimpleTypeInAggregateRoot(IMemberInfo internalRecordset, object obj,
                                                              IDictionary<int, T> aggregateRoot)
            {
                int objectHash = obj.GetObjectsHashCodeByPrimaryKeys();

                // Find the member(s) in aggregateRoot that contains this member
                foreach (
                    T o in
                        aggregateRoot.Values.AsEnumerable().Where(
                            x =>
                            internalRecordset.GetValue(x) != null &&
                            internalRecordset.GetValue(x).GetObjectsHashCodeByPrimaryKeys() == objectHash))
                    internalRecordset.SetValue(o, obj);
            }

            private IEnumerable<T> HydrateRecordset(IDataReader dataReader)
            {
                Func<IDictionary<string, object>, T> hydratorFunction = GetHydratorFunction();

                while (dataReader.Read())
                {
                    T hydratedObject = hydratorFunction(dataReader.ToDictionary());
                    yield return GetHydratedObjectFromCache(hydratedObject);
                }
            }

            [NotNull]
            private Func<IDictionary<string, object>, T> GetHydratorFunction()
            {
                IObjectHydrator<T> hydrator = GetHydrator();
                return hydrator == null
                           ? (Func<IDictionary<string, object>, T>)
                             (x => _defaultObjectHydrator.Hydrate<T>(x))
                           : (hydrator.Hydrate);
            }

            [CanBeNull]
            private static IObjectHydrator<T> GetHydrator()
            {
                var objectHydratorAttribute = typeof(T).GetAttribute<ObjectHydratorAttribute>();
                return objectHydratorAttribute == null
                           ? null
                           : objectHydratorAttribute.HydratorType.ConstructUsingDefaultConstructor
                                 <IObjectHydrator<T>>();
            }

            private T GetHydratedObjectFromCache(T hydratedObject)
            {
                if (_hydratedObjects.Contains(hydratedObject))
                    return
                        (_hydratedObjects[hydratedObject].Target ??
                          (_hydratedObjects[hydratedObject].Target = hydratedObject)) as T;

                _hydratedObjects.Add(hydratedObject);
                return hydratedObject;
            }
        }
    }
}