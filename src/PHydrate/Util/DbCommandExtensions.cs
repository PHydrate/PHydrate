using System.Collections.Generic;
using System.Data;

namespace PHydrate.Util
{
    /// <summary>
    /// Extensions on IDbCommand
    /// </summary>
    public static class DbCommandExtensions
    {
        /// <summary>
        /// Adds a parameter to the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameter">The parameter.</param>
        public static void AddParameter(this IDbCommand command, KeyValuePair<string, object> parameter)
        {
            AddParameter( command, parameter, ParameterDirection.Input );
        }

        /// <summary>
        /// Adds a parameter to the command with a specified direction.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="direction">The direction.</param>
        public static void AddParameter(this IDbCommand command, KeyValuePair<string, object> parameter, ParameterDirection direction)
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
        public static void AddParameter<T>(this IDbCommand command, string parameterName, ParameterDirection direction)
        {
            AddParameter( command, new KeyValuePair< string, object >( parameterName, default(T) ), direction );
        }
    }
}