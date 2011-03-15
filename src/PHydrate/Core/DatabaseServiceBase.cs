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
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using PHydrate.Attributes;
using PHydrate.Util;

namespace PHydrate.Core
{
    /// <summary>
    /// Base implementation of IDatabaseService
    /// </summary>
    public abstract class DatabaseServiceBase : IDatabaseService
    {
        #region Implementation of IDatabaseService

        /// <summary>
        /// Executes a stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="dataParameters">The data parameters.</param>
        /// <returns>An IDataReader containing the results.</returns>
        public IDataReader ExecuteStoredProcedureReader( string storedProcedureName,
                                                         IEnumerable< KeyValuePair< string, object > > dataParameters )
        {
            IDbConnection dbConnection = GetDbConnection();

            using ( IDbCommand command = dbConnection.CreateCommand() )
            {
                SetupCommand( command, storedProcedureName, dataParameters );
                return command.ExecuteReader();
            }
        }

        /// <summary>
        /// Executes a stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>An IDataReader containing the results.</returns>
        public IDataReader ExecuteStoredProcedureReader( string storedProcedureName )
        {
            return ExecuteStoredProcedureReader( storedProcedureName, null );
        }

        /// <summary>
        /// Executes the stored procedure, returning the first column of the first record.
        /// </summary>
        /// <typeparam name="T">The type to cast the return value to.</typeparam>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns></returns>
        public T ExecuteStoredProcedureScalar< T >( string storedProcedureName )
        {
            return ExecuteStoredProcedureScalar< T >( storedProcedureName, null );
        }

        /// <summary>
        /// Executes the stored procedure, returning the first column of the first record.
        /// </summary>
        /// <typeparam name="T">The type to cast the return value to.</typeparam>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="dataParameters">The data parameters.</param>
        /// <returns></returns>
        public T ExecuteStoredProcedureScalar< T >( string storedProcedureName,
                                                    IEnumerable< KeyValuePair< string, object > > dataParameters )
        {
            IDbConnection dbConnection = GetDbConnection();

            using ( IDbCommand dbCommand = dbConnection.CreateCommand() )
            {
                SetupCommand( dbCommand, storedProcedureName, dataParameters );
                return (T)dbCommand.ExecuteScalar();
            }
        }

        private static void SetupCommand( IDbCommand command, string storedProcedureName,
                                          IEnumerable< KeyValuePair< string, object > > dataParameters )
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedureName;
            if ( dataParameters != null )
                foreach ( var parameter in dataParameters )
                    command.AddParameter( parameter );
        }

        [ NotNull ]
        private IDbConnection GetDbConnection()
        {
            IDbConnection dbConnection = GetDatabaseConnection();
            if ( dbConnection.State != ConnectionState.Open )
                dbConnection.Open();
            return dbConnection;
        }

        #endregion

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>The driver-specific connection object</returns>
        [ SuppressMessage( "Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate" ) ]
        protected abstract IDbConnection GetDatabaseConnection();
    }
}