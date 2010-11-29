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

using System.Collections.Generic;
using System.Data;
using PHydrate.Util;

namespace PHydrate.Core
{
    /// <summary>
    /// Base implementation of IDatabaseService
    /// </summary>
    public abstract class DatabaseServiceBase : IDatabaseService
    {
        #region Implementation of IDatabaseService

        public IDataReader ExecuteStoredProcedureReader( string storedProcedureName,
                                                         IDictionary<string, object> dataParameters )
        {
            IDbConnection dbConnection = GetConnection();
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();

            using ( IDbCommand command = dbConnection.CreateCommand() )
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                if (dataParameters != null)
                    foreach (var parameter in dataParameters)
                        command.AddParameter( parameter );
                return command.ExecuteReader( CommandBehavior.CloseConnection );
            }
        }

        public IDataReader ExecuteStoredProcedureReader(string storedProcedureName)
        {
            return ExecuteStoredProcedureReader( storedProcedureName, null );
        }

        #endregion

        protected abstract IDbConnection GetConnection();
    }
}