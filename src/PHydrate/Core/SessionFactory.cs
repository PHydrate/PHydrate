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

namespace PHydrate.Core
{
    /// <summary>
    /// Default implementation of ISessionFactory
    /// </summary>
    public sealed class SessionFactory : ISessionFactory
    {
        private readonly IDatabaseService _databaseService;

        private static SessionFactory _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionFactory"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        internal SessionFactory(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Creates an instance of the SessionFactory.
        /// </summary>
        /// <returns></returns>
        public static ISessionFactory Create()
        {
            return _instance ?? ( _instance = null ); // TODO
        }

        #region Implementation of ISessionFactory

        /// <summary>
        /// Gets the global transaction.
        /// </summary>
        /// <value>The global transaction.</value>
        public ITransaction GlobalTransaction
        {
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
            return new Session( _databaseService );
        }

        #endregion
    }
}