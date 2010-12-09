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
        IDataReader ExecuteStoredProcedureReader( string storedProcedureName, IEnumerable< KeyValuePair< string, object > > dataParameters );

        /// <summary>
        /// Executes a stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>An IDataReader containing the results.</returns>
        IDataReader ExecuteStoredProcedureReader( string storedProcedureName );

        /// <summary>
        /// Executes the stored procedure, returning the first column of the first record.
        /// </summary>
        /// <typeparam name="T">The type to cast the return value to.</typeparam>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns></returns>
        T ExecuteStoredProcedureScalar< T >( string storedProcedureName );

        /// <summary>
        /// Executes the stored procedure scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="dataParameters">The data parameters.</param>
        /// <returns></returns>
        T ExecuteStoredProcedureScalar< T >( string storedProcedureName, IEnumerable< KeyValuePair< string, object > > dataParameters );
    }
}