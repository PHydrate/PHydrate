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
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace PHydrate.Specifications
{
    /// <summary>
    /// A specialized specification class for providing a boolean <see cref="Expression"/> to be used to filter results from the database
    /// </summary>
    /// <remarks>
    /// The parsed expression will be used to pass parameters to the stored procedure.
    /// You should only use object members that are supported by the underlying stored procedure.
    /// </remarks>
    /// <typeparam name="T">The type this specification accepts</typeparam>
    public abstract class DbSpecification<T> : ISpecification<T>
    {
        private Func<T, bool> _compliedSpec;

        /// <summary>
        /// Gets an <see cref="Expression"/> that will be parsed to send parameters to the stored procedure.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public abstract Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// Returns true if the entity passes the specification
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if the entity passes the specification; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsSatisfiedBy(T entity)
        {
            if (_compliedSpec == null)
                _compliedSpec = Criteria.Compile();
            return _compliedSpec(entity);
        }

        /// <summary>
        /// Returns a new DBSpecification that combines two specifications.
        /// </summary>
        /// <param name="otherSpecification">The other specification.</param>
        /// <returns></returns>
        public DbSpecification<T> And(DbSpecification<T> otherSpecification)
        {
            return new CombinedDbSpecification<T>(this, otherSpecification, ExpressionType.AndAlso);
        }

        /// <summary>
        /// Returns a new DBSpecification that combines two specifications.
        /// </summary>
        /// <param name="otherSpecification">The other specification.</param>
        /// <returns></returns>
        public DbSpecification<T> Or(DbSpecification<T> otherSpecification)
        {
            return new CombinedDbSpecification<T>(this, otherSpecification, ExpressionType.OrElse);
        }

        /// <summary>
        /// Inverts the value of the DbSpecification
        /// </summary>
        /// <returns></returns>
        public DbSpecification<T> Not()
        {
            return new NotDbSpecification<T>(this);
        }
    }
}