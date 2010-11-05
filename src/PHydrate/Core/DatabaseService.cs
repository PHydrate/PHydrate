using System;
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
        public DatabaseService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        #region Implementation of IDatabaseService

        public IDataReader ExecuteStoredProcedureReader( string storedProcedureName, params IDataParameter[] dataParameters )
        {
            using (IDbCommand command = _dbConnection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                foreach (IDataParameter dataParameter in dataParameters)
                    command.Parameters.Add( dataParameter );
                return command.ExecuteReader( CommandBehavior.CloseConnection );
            }
        }

        #endregion
    }
}