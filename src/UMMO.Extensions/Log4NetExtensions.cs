#region Copyright

// This file is part of UMMO.
// 
// UMMO is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// UMMO is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with UMMO.  If not, see <http://www.gnu.org/licenses/>.
//  
// Copyright 2010, Stephen Michael Czetty

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using log4net;
using log4net.Core;

namespace UMMO.Extensions
{
    /// <summary>
    /// Extensions on Log4Net ILog
    /// </summary>
    public static class Log4NetExtensions
    {
        private static readonly Stack< KeyValuePair< string, string > > MethodStack =
            new Stack< KeyValuePair< string, string > >();

        static Log4NetExtensions()
        {
            // Set the default log level for method wrappers.
            MethodLogLevel = Level.Debug;

            // Default string to log method entry/exit
            MethodLogFormatString = "{0}ing method {1}";
        }

        /// <summary>
        /// Logs the method.
        /// </summary>
        /// <remarks>
        /// This method is intended to be called inside of a using() block.  When the block is exited,
        /// the event will automatically be logged, and the method stack will be popped.
        /// </remarks>
        /// <param name="log">The log4net ILog object.</param>
        /// <returns>IDisposable for the using() block.</returns>
        public static IDisposable LogMethod(this ILog log)
        {
            MethodBase callingMethod = GetCallingMethod();
            string fullyQualifiedName = callingMethod.DeclaringType.FullName + "." + callingMethod.Name;
            MethodStack.Push( new KeyValuePair< string, string >( fullyQualifiedName, callingMethod.Name ) );


            log.Logger.Log( callingMethod.DeclaringType, MethodLogLevel,
                            String.Format( MethodLogFormatString, "Enter",
                                           fullyQualifiedName,
                                           callingMethod.Name ), null );

            return new Log4NetWrapper( log, callingMethod );
        }

        /// <summary>
        /// Gets or sets the method log format string.
        /// </summary>
        /// <value>The method log format string.</value>
        /// <remarks>
        /// The format string replacements are:
        /// {0} - Event (Enter, Exit)
        /// {1} - Fully qualified name of the method
        /// {2} - Short name of the method
        /// {3} - Parameters (comma seperated) (TODO)
        /// </remarks>
        public static string MethodLogFormatString { get; set; }

        /// <summary>
        /// Gets or sets the method log level.
        /// </summary>
        /// <remarks>
        /// The default log level is Level.Debug.
        /// </remarks>
        /// <value>The method log level.</value>
        public static Level MethodLogLevel { get; set; }

        private static MethodBase GetCallingMethod()
        {
            return new StackTrace().GetFrame( 2 ).GetMethod();
        }

        #region Log4NetWrapper implementation of IDisposable

        private class Log4NetWrapper : IDisposable
        {
            private readonly ILog _log;
            private readonly MethodBase _callingMethod;

            internal Log4NetWrapper( ILog log, MethodBase callingMethod )
            {
                _log = log;
                _callingMethod = callingMethod;
            }

            #region Implementation of IDisposable

            public void Dispose()
            {
                var exitingMethod = MethodStack.Pop();
                _log.Logger.Log( _callingMethod.DeclaringType, MethodLogLevel,
                                 String.Format( MethodLogFormatString, "Exit", exitingMethod.Key, exitingMethod.Value ),
                                 null );
            }

            #endregion
        }

        #endregion
    }
}