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
// Copyright 2010, Stephen Michael Czetty
// 

#endregion

using System;
using System.Collections.Generic;
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
        /// <param name="defaultObjectHydrator">The object hydrator to use by default</param>
        internal Session( IDatabaseService databaseService, IObjectHydrator defaultObjectHydrator )
        {
            _databaseService = databaseService;
            _defaultObjectHydrator = defaultObjectHydrator;
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
        {
            // Get the name of the stored procedure that will hydrate this object
            var hydrationAttribute = typeof(T).GetAttribute< HydrateUsingAttribute >();
            if ( hydrationAttribute == null || String.IsNullOrEmpty( hydrationAttribute.ProcedureName ) )
                throw new PHydrateException(
                    String.Format(
                        "Unable to get object of type {0}.  Define a hydration procedure with [HydrateUsing]",
                        typeof(T).FullName ) );

            return HydrateFromStoredProcedure( hydrationAttribute, query );
        }

        /// <summary>
        /// Gets an object of type <typeparamref name="T"/> given the specification.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <returns>The found object, or null.</returns>
        public IEnumerable< T > Get< T >( ISpecification< T > specification )
        {
            IEnumerable< T > foundObjects;

            if ( specification is IDbSpecification< T > )
                foundObjects = Get( ( (IDbSpecification< T >)specification ).Criteria );
            else
                foundObjects = Get< T >( null as Expression< Func< T, bool > > );

            Func< T, bool > satisifies = x => true;
            if ( specification is IExplicitSpecification< T > )
                satisifies = ( (IExplicitSpecification< T >)specification ).Satisfies;

            return foundObjects.Where( obj => satisifies( obj ) );
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

        private IEnumerable< T > HydrateFromStoredProcedure< T >( CrudAttributeBase hydrationAttribute,
                                                                  Expression< Func< T, bool > > query )
        {
            var dataReader = _databaseService.ExecuteStoredProcedureReader( hydrationAttribute.ProcedureName,
                                                                            query.GetDataParameters() );

            // TODO: Fix IObjectHydrator so Default and other hydrators don't have different interfaces
            IObjectHydrator< T > hydrator = GetHydrator< T >();
            while ( dataReader.Read() )
            {
                yield return hydrator == null
                                 ? _defaultObjectHydrator.Hydrate< T >( dataReader.ToDictionary() )
                                 : hydrator.Hydrate( dataReader.ToDictionary() );
            }
        }

        private static IObjectHydrator< T > GetHydrator< T >()
        {
            var objectHydratorAttribute = typeof(T).GetAttribute< ObjectHydratorAttribute >();
            if ( objectHydratorAttribute == null )
                return null;

            try
            {
                var hydrator =
                    objectHydratorAttribute.HydratorType.ConstructUsingDefaultConstructor< IObjectHydrator< T > >();

                return hydrator;
            }
            catch ( PHydrateInternalException )
            {
                // No default constructor found.  Promote to a PHydrateException
                // TODO: Remove this restriction to enable dependency injection.  Perhaps hook in structuremap or another IoC container?
                throw new PHydrateException(
                    String.Format(
                        "Could not construct custom Hydrator {0}.  Class must have a default constructor!",
                        objectHydratorAttribute.HydratorType.FullName ) );
            }
        }

        #endregion
    }
}