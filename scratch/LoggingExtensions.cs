using System;
using System.Data;
using BancVue.Core.Common.Utils;
using log4net;


namespace BancVue.Core.Common.Logging
{
	public static class LoggingExtensions
	{
		public static void LogDbCommand( this ILog log, IDbCommand command )
		{
			// Log command call.
			log.Debug( "SQL COMMAND: \n" + command.CommandText );

			// Log parameters.
			foreach ( IDataParameter parameter in command.Parameters )
				log.Debug( String.Format( "SQL PARAM: Name='{0}' Value='{1}'", parameter.ParameterName, parameter.Value ) );
		}


// 		public static void MethodStart( this ILog log, params Expression< Func< string, object > >[] paramExpressions )
// 		{
// 			var parameterEntries = new List< string >();
// 			foreach ( KeyValuePair< string, object > keyValuePair in ReflectionHelper.GetNameValuePairsFromLambdas( paramExpressions ) )
// 				parameterEntries.Add( keyValuePair.Key + "=" + keyValuePair.Value );
// 			log.Debug( "Start: " + ReflectionHelper.GetCallingMethod().Name + "( " + parameterEntries.AsDelimitedString( ", " ) + " );" );
// 		}
		public static void Parameter( this ILog log, string name, params string[] values )
		{
			log.Debug( "   parameter: " + name + " = { " + values.AsDelimitedString( ", " ) + " }" );
		}


		public static void Variable( this ILog log, string name, params string[] values )
		{
			log.Debug( "   variable: " + name + " = { " + values.AsDelimitedString( ", " ) + " }" );
		}


		public static void MethodStart( this ILog log )
		{
			log.Debug( "Start: " + ReflectionHelper.GetCallingMethod().Name );
		}


		public static void MethodFinish( this ILog log )
		{
			log.Debug( "Finish: " + ReflectionHelper.GetCallingMethod().Name );
		}
	}
}