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

namespace PHydrate.Util
{
    /// <summary>
    /// Extensions on IDbCommand
    /// </summary>
    [ SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Db" ) ]
    public static class DbCommandExtensions
    {
        /// <summary>
        /// Adds a parameter to the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameter">The parameter.</param>
        public static void AddParameter( this IDbCommand command, KeyValuePair< string, object > parameter )
        {
            AddParameter( command, parameter, ParameterDirection.Input );
        }

        /// <summary>
        /// Adds a parameter to the command with a specified direction.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="direction">The direction.</param>
        private static void AddParameter( this IDbCommand command, KeyValuePair< string, object > parameter,
                                          ParameterDirection direction )
        {
            IDataParameter dataParameter = command.CreateParameter();
            dataParameter.ParameterName = parameter.Key;
            dataParameter.Value = parameter.Value;
            dataParameter.Direction = direction;
            command.Parameters.Add( dataParameter );
        }

        /// <summary>
        /// Adds a parameter to the command with a specified direction and a default value.
        /// </summary>
        /// <typeparam name="T">The type of the parameter</typeparam>
        /// <param name="command">The command.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="direction">The direction.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static void AddParameter< T >( this IDbCommand command, string parameterName,
                                              ParameterDirection direction )
        {
            command.AddParameter( new KeyValuePair< string, object >( parameterName, default( T ) ), direction );
        }
    }
}