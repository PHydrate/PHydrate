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
using System.Linq.Expressions;
using PHydrate.Attributes;
using PHydrate.Util;
using PHydrate.Util.MemberInfoWrapper;

namespace PHydrate.Core
{
    /// <summary>
    /// Implementation of ISession
    /// </summary>
    public class Session : ISession
    {
        private readonly IDatabaseService _databaseService;
        private readonly IDefaultObjectHydrator _defaultObjectHydrator;
        private readonly WeakReferenceObjectCache _hydratedObjects = new WeakReferenceObjectCache();
        private readonly string _parameterPrefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        /// <param name="defaultObjectHydrator">The object hydrator to use by default</param>
        /// <param name="parameterPrefix">The string to prepend to parameter names</param>
        internal Session( IDatabaseService databaseService, IDefaultObjectHydrator defaultObjectHydrator,
                          string parameterPrefix )
        {
            _databaseService = databaseService;
            _defaultObjectHydrator = defaultObjectHydrator;
            _parameterPrefix = parameterPrefix;
        }

        #region Implementation of ISession

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <value>The transaction.</value>
        public ITransaction Transaction
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets an object of type <typeparamref name="T"/> given the arguments in the query.
        /// </summary>
        /// <typeparam name="T">The type of object to return.</typeparam>
        /// <param name="query">The parameters used to select the object.</param>
        /// <returns>The found object, or null if not found.</returns>
        public IEnumerable< T > Get< T >( Expression< Func< T, bool > > query )
            where T : class
        {
            // Get the name of the stored procedure that will hydrate this object
            HydrateUsingAttribute hydrationAttribute = GetCrudAttributeFromType< T, HydrateUsingAttribute >();

            return HydrateFromStoredProcedure( hydrationAttribute, query );
        }

        private static TAttribute GetCrudAttributeFromType< T, TAttribute >() where TAttribute : CrudAttributeBase
        {
            var crudAttribute = typeof(T).GetAttribute< TAttribute >();
            if ( crudAttribute == null || String.IsNullOrEmpty( crudAttribute.ProcedureName ) )
                throw new PHydrateException(
                    "Unable to process object of type {0}.  Define a stored procedure with [{0}]",
                    typeof(T).FullName, typeof(TAttribute).Name.Replace( "Attribute", "" ) );

            return crudAttribute;
        }

        private IEnumerable< T > HydrateFromStoredProcedure< T >( CrudAttributeBase hydrationAttribute,
                                                                  Expression< Func< T, bool > > query )
            where T : class
        {
            IDataReader dataReader = _databaseService.ExecuteStoredProcedureReader( hydrationAttribute.ProcedureName,
                                                                                    query.GetDataParameters(
                                                                                        _parameterPrefix ) );

            return new DataHydrator<T>(_defaultObjectHydrator, _hydratedObjects).HydrateFromDataReader( dataReader );
        }

        /// <summary>
        /// Gets an object of type <typeparamref name="T"/> given the specification.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <returns>The found object, or null.</returns>
        public IEnumerable< T > Get< T >( ISpecification< T > specification )
            where T : class
        {
            IEnumerable< T > foundObjects = GetItemsFromDbSpecification( specification );

            return FilterUsingExplicitSpecification( specification, foundObjects );
        }

        private IEnumerable< T > GetItemsFromDbSpecification< T >( ISpecification< T > specification ) where T : class
        {
            Expression< Func< T, bool > > criteria = specification is IDbSpecification< T >
                                                         ? ( (IDbSpecification< T >)specification ).Criteria
                                                         : null;
            return Get( criteria );
        }

        private static IEnumerable< T > FilterUsingExplicitSpecification< T >( ISpecification< T > specification,
                                                                               IEnumerable< T > foundObjects )
        {
            Func< T, bool > satisifies = x => true;
            if ( specification is IExplicitSpecification< T > )
                satisifies = ( (IExplicitSpecification< T >)specification ).Satisfies;

            return foundObjects.Where( obj => satisifies( obj ) );
        }

        /// <summary>
        /// Deletes the specified object from the database store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToDelete">The object to delete.</param>
        public void Delete< T >( T objectToDelete )
            where T : class
        {
            long recordsAffected =
                ExecuteIntegerProcedureFromAttribute< T, DeleteUsingAttribute >( objectToDelete.GetDataParameters(
                    _parameterPrefix ) );
            if ( recordsAffected == 0 )
                throw new PHydrateException( "Delete of object failed." );
            // TODO: Need a runtime warning when recordsAffected > 1 here.  Figure out how to handle those!

            _hydratedObjects.Remove( objectToDelete );
        }

        private long ExecuteIntegerProcedureFromAttribute< T, TAttribute >(
            IEnumerable< KeyValuePair< string, object > > keyValuePairs )
            where T : class where TAttribute : CrudAttributeBase
        {
            TAttribute crudAttribute = GetCrudAttributeFromType< T, TAttribute >();

            return _databaseService.ExecuteStoredProcedureScalar< long >( crudAttribute.ProcedureName,
                                                                          keyValuePairs );
        }

        /// <summary>
        /// Persists the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the object to persist.</typeparam>
        /// <param name="objectToPersist">The object to persist.</param>
        public void Persist< T >( T objectToPersist )
            where T : class
        {
            if ( _hydratedObjects.Contains( objectToPersist ) )
                UpdateObject( objectToPersist );
            else
                InsertObject( objectToPersist );
        }

        private void UpdateObject< T >( T objectToPersist )
            where T : class
        {
            long recordsAffected =
                ExecuteIntegerProcedureFromAttribute< T, UpdateUsingAttribute >( objectToPersist.GetDataParameters(
                    _parameterPrefix ) );
            if ( recordsAffected == 0 )
                throw new PHydrateException( "Update of object failed." );
            // TODO: Need a runtime warning when recordsAffected > 1 here.  Figure out how to handle those!
        }

        private void InsertObject< T >( T objectToPersist )
            where T : class
        {
            CreateUsingAttribute createAttribute = GetCrudAttributeFromType< T, CreateUsingAttribute >();

            var primaryKeyValue = _databaseService.ExecuteStoredProcedureScalar< object >(
                createAttribute.ProcedureName,
                objectToPersist.GetDataParameters( _parameterPrefix ) );

            objectToPersist.SetPropertyValueWithAttribute< T, PrimaryKeyAttribute >( primaryKeyValue );

            _hydratedObjects.Add( objectToPersist );
        }

        #endregion

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
                IEnumerable<IMemberInfo> internalRecordsets = typeof(T).GetMembersWithAttribute<RecordsetAttribute>();
                return internalRecordsets.Any()
                           ? HydrateRecordsetWithInternals(dataReader, internalRecordsets)
                           : HydrateRecordset(dataReader);
            }

            private IEnumerable<T> HydrateRecordsetWithInternals(IDataReader dataReader, IEnumerable<IMemberInfo> internalRecordsets)
            {
                IDictionary< int, T > aggregateRoot =
                    HydrateRecordset( dataReader ).ToDictionary( x => x.GetObjectsHashCodeByPrimaryKeys() );

                IList< string > primaryKeyMembers =
                    typeof(T).GetMembersWithAttribute< PrimaryKeyAttribute >().Select( x => x.Wrapped.Name ).ToArray();

                foreach ( IMemberInfo internalRecordset in internalRecordsets )
                {
                    if ( !dataReader.NextResult() ) // TODO: Throw an exception?
                        break;

                    // TODO
                    // Assign values to appropriate member in aggregateRoot
                    // Support single object, IEnumerable, and IList for now.  IDictionary later.
                    IEnumerable enumerator =
                        internalRecordset.Type.ExecuteGenericMethod< DataHydrator< T >, IEnumerable >(
                            x => x.HydrateFromDataReader( dataReader ),
                            _defaultObjectHydrator,
                            _hydratedObjects
                            );
                    foreach ( object obj in enumerator )
                    {
                        int lookupHash = internalRecordset.GetLookupHash< T >( obj, primaryKeyMembers );

                        if ( !aggregateRoot.ContainsKey( lookupHash ) )
                            continue;

                        // BUG: This will not work with superclasses
                        if ( internalRecordset.Type == obj.GetType() &&
                             internalRecordset.GetValue( aggregateRoot[ lookupHash ] ) == null ) // Simple type
                            internalRecordset.SetValue( aggregateRoot[ lookupHash ], obj );
                    }
                }
                return aggregateRoot.Values;
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

            private Func<IDictionary<string, object>, T> GetHydratorFunction()
            {
                IObjectHydrator<T> hydrator = GetHydrator();
                return hydrator == null
                           ? (Func<IDictionary<string, object>, T>)
                             (x => _defaultObjectHydrator.Hydrate<T>(x))
                           : (hydrator.Hydrate);
            }

            private static IObjectHydrator<T> GetHydrator()
            {
                var objectHydratorAttribute = typeof(T).GetAttribute<ObjectHydratorAttribute>();
                return objectHydratorAttribute == null
                           ? null
                           : objectHydratorAttribute.HydratorType.ConstructUsingDefaultConstructor<IObjectHydrator<T>>();
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