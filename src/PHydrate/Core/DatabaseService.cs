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

using System.Data;

namespace PHydrate.Core
{
    /// <summary>
    /// Implementation of IDatabaseService for Sql Server
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseService"/> class.
        /// </summary>
        /// <param name="dbConnection">The db connection.</param>
        public DatabaseService( IDbConnection dbConnection )
        {
            _dbConnection = dbConnection;
        }

        #region Implementation of IDatabaseService

        public IDataReader ExecuteStoredProcedureReader( string storedProcedureName,
                                                         params IDataParameter[] dataParameters )
        {
            using ( IDbCommand command = _dbConnection.CreateCommand() )
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                foreach ( IDataParameter dataParameter in dataParameters )
                    command.Parameters.Add( dataParameter );
                IDataReader executeStoredProcedureReader = command.ExecuteReader( CommandBehavior.CloseConnection );
                return executeStoredProcedureReader;
            }
        }

        #endregion
    }
}