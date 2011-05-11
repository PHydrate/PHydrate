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
using log4net;
using log4net.Core;

namespace PHydrate.Aspects.Logging
{
    /// <summary>
    /// Class created by amendments for logging
    /// </summary>
    /// <typeparam name="T">The class being logged</typeparam>
    public static class Logger< T >
    {
        private static readonly ILog Log;
        private static readonly Level LoggingLevel = Level.Debug;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger&lt;T&gt;"/> class, using the default log level of Debug.
        /// </summary>
        static Logger()
        {
            Log = LogManager.GetLogger( typeof(T) );
            LoggingLevel = Level.Debug;
        }

        /// <summary>
        /// Log the start of the method.
        /// </summary>
        /// <param name="instance">The instance of the class</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        public static void BeginMethod( T instance, string methodName, object[] parameters )
        {
            DoLog( "Entering method: " + methodName );
            if ( parameters.Length == 0 )
                return;

            int parameterCount = 0;
            DoLog( "Parameters:" );
            foreach (object o in parameters)
            {
                string parameterType = o == null ? "<null>" : o.GetType().Name;
                DoLog( String.Format( "{0}: ({1}) {2}", parameterCount++, parameterType, o ?? "<null>" ) );
            }
        }

        private static void DoLog( string stringToLog )
        {
            Log.Logger.Log( typeof(T), LoggingLevel, stringToLog, null );
        }

        /// <summary>
        /// Log the end of the method.
        /// </summary>
        /// <param name="instance">The instance of the class</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">Parameters passed to the method call</param>
        public static void EndMethod( T instance, string methodName, object[] parameters )
        {
            DoLog( "Leaving method: " + methodName );
        }
    }
}