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
using System.Diagnostics.CodeAnalysis;

namespace PHydrate.Core
{
    /// <summary>
    /// Default implementation of ISessionFactory
    /// </summary>
    public sealed class SessionFactory : ISessionFactory
    {
        private readonly IDatabaseService _databaseService;
        private readonly string _parameterPrefix;
        private readonly IDefaultObjectHydrator _defaultObjectHydrator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionFactory"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        /// <param name="parameterPrefix">The prefix to place in front of parameter names.</param>
        /// <param name="defaultObjectHydrator">The default object hydrator to use, or null to use the built-in version</param>
        internal SessionFactory(IDatabaseService databaseService, string parameterPrefix, IDefaultObjectHydrator defaultObjectHydrator)
        {
            _databaseService = databaseService;
            _parameterPrefix = parameterPrefix;
            _defaultObjectHydrator = defaultObjectHydrator ?? new DefaultObjectHydrator();
        }

        #region Implementation of ISessionFactory

        /// <summary>
        /// Gets the global transaction.
        /// </summary>
        /// <value>The global transaction.</value>
        public ITransaction GlobalTransaction
        {
            [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns>
        /// An implementation of ISession associated with this factory.
        /// </returns>
        public ISession GetSession()
        {
            return new Session( _databaseService, _defaultObjectHydrator, _parameterPrefix );
        }

        #endregion
    }
}