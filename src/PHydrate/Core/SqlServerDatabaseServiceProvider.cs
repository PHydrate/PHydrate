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
using System.Data;
using System.Data.SqlClient;

namespace PHydrate.Core
{
    /// <summary>
    /// MS Sql Server implementation of IDatabaseServiceProvider
    /// </summary>
    public sealed class SqlServerDatabaseServiceProvider : IDatabaseServiceProvider
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDatabaseServiceProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SqlServerDatabaseServiceProvider( string connectionString )
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get an instance of the database service.
        /// </summary>
        /// <returns></returns>
        public IDatabaseService DatabaseService()
        {
            return new DatabaseService( new SqlConnection( _connectionString ) );
        }
    }
}