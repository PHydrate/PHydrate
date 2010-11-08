using System;
using System.Linq;
using System.Linq.Expressions;
using PHydrate.Attributes;
using PHydrate.Util;

namespace PHydrate.Core
{
    /// <summary>
    /// Implementation of ISession
    /// </summary>
    public class Session : ISession
    {
        private readonly IDatabaseService _databaseService;
        private readonly IObjectHydrator _defaultObjectHydrator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        public Session(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _defaultObjectHydrator = new DefaultObjectHydrator();
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
        public T Get< T >( Expression< Func< T, bool > > query )
        {
            // Get the name of the stored procedure that will hydrate this object
            var hydrationAttribute = typeof(T).GetAttribute< HydrateUsingAttribute >();
            if (hydrationAttribute == null || String.IsNullOrEmpty(hydrationAttribute.ProcedureName))
                throw new PHydrateException(
                    String.Format(
                        "Unable to get object of type {0}.  Define a hydration procedure with [HydrateUsing]",
                        typeof(T).FullName ) );

            return HydrateFromStoredProcedure( hydrationAttribute, query );
        }

        private T HydrateFromStoredProcedure< T >( CrudAttributeBase hydrationAttribute, Expression< Func< T, bool > > query )
        {
            var dataReader = _databaseService.ExecuteStoredProcedureReader( hydrationAttribute.ProcedureName,
                                                                            query.GetDataParameters() );

            if (dataReader.Read())
            {
                // TODO: Fix IObjectHydrator so Default and other hydrators don't have different interfaces
                IObjectHydrator< T > hydrator = GetHydrator< T >();
                return hydrator == null
                           ? _defaultObjectHydrator.Hydrate< T >( dataReader.ToDictionary() )
                           : hydrator.Hydrate( dataReader.ToDictionary() );
            }

            return default(T);
        }

        private IObjectHydrator<T> GetHydrator< T >()
        {
            return null;
        }

        /// <summary>
        /// Persists the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the object to persist.</typeparam>
        /// <param name="objectToPersist">The object to persist.</param>
        public void Persist< T >( T objectToPersist )
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}