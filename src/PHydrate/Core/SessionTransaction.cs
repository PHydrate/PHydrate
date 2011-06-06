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

using System.Collections.Generic;
using System.Data;
using PHydrate.Util;

namespace PHydrate.Core
{
    /// <summary>
    /// Implementation of <see cref="ITransaction" /> for session-level transactions.
    /// </summary>
    public class SessionTransaction : ITransaction
    {
        private readonly IDatabaseService _databaseService;
        // TODO: Need a specialized stack that is thread-aware!
        private readonly Stack< IDbTransaction > _transactionStack = new Stack< IDbTransaction >();

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionTransaction"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        public SessionTransaction( IDatabaseService databaseService )
        {
            _databaseService = databaseService;
        }

        /// <summary>
        ///   Begins this transaction.
        /// </summary>
        public void Begin()
        {
            IDbTransaction transaction = _databaseService.BeginTransaction();
            lock ( _transactionStack )
                _transactionStack.Push( transaction );
        }

        /// <summary>
        ///   Commits this transaction.
        /// </summary>
        public void Commit()
        {
            IDbTransaction transaction = _transactionStack.TryPop();
            if ( transaction != null )
                transaction.Commit();
        }

        /// <summary>
        ///   Rolls back this transaction.
        /// </summary>
        public void Rollback()
        {
            IDbTransaction transaction = _transactionStack.TryPop();
            if ( transaction != null )
                transaction.Rollback();
        }

        /// <summary>
        /// Rollback any uncommited transactions
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            IDbTransaction transaction = _transactionStack.TryPop();
            while ( transaction != null )
                transaction.Rollback();
        }
    }
}