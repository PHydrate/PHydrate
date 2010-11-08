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
    public class SessionFactory : ISessionFactory
    {
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
            throw new NotImplementedException();
        }

        #endregion
    }
}