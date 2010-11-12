using System.Collections.Generic;
using System.Data;

namespace PHydrate.Core
{
    /// <summary>
    /// Interface to the database.
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Executes a stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="dataParameters">The data parameters.</param>
        /// <returns>An IDataReader containing the results.</returns>
        IDataReader ExecuteStoredProcedureReader( string storedProcedureName, IDictionary<string, object> dataParameters );

        /// <summary>
        /// Executes a stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>An IDataReader containing the results.</returns>
        IDataReader ExecuteStoredProcedureReader( string storedProcedureName );
    }
}