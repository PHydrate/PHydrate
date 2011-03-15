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
using System.Diagnostics.CodeAnalysis;

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
        [ SuppressMessage( "Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures" ) ]
        IDataReader ExecuteStoredProcedureReader( string storedProcedureName,
                                                  IEnumerable< KeyValuePair< string, object > > dataParameters );

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
        [ SuppressMessage( "Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures" ) ]
        T ExecuteStoredProcedureScalar< T >( string storedProcedureName,
                                             IEnumerable< KeyValuePair< string, object > > dataParameters );
    }
}