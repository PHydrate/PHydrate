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
using System.Runtime.InteropServices;
using System.Security.Permissions;
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

            // Set the default log level for exceptions.
            ExceptionLogLevel = Level.Error;

            // Default string to log method entry/exit
            MethodLogFormatString = "{0} method {1}";

            // Default string to log an exception
            ExceptionLogFormatString = "Exception thrown in method {0}";
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
        public static IExceptionLogger LogMethod(this ILog log)
        {
            MethodBase callingMethod = GetCallingMethod();
            string fullyQualifiedName = callingMethod.DeclaringType.FullName + "." + callingMethod.Name;
            MethodStack.Push( new KeyValuePair< string, string >( fullyQualifiedName, callingMethod.Name ) );


            log.Logger.Log( callingMethod.DeclaringType, MethodLogLevel,
                            String.Format( MethodLogFormatString, "Entering",
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
        /// {0} - ActionVerb (Entering, Exiting, Exception)
        /// {1} - Fully qualified name of the method
        /// {2} - Short name of the method
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

        /// <summary>
        /// Gets or sets the exception log format string.
        /// </summary>
        /// <value>The exception log format string.</value>
        /// <remarks>
        /// The fomat string replacements are:
        /// {0} - Fully qualified name of the method
        /// {1} - Short name of the method
        /// </remarks>
        public static string ExceptionLogFormatString { get; set; }

        /// <summary>
        /// Gets or sets the exception log level.
        /// </summary>
        /// <remarks>
        /// The default log level is Level.Error
        /// </remarks>
        /// <value>The exception log level.</value>
        public static Level ExceptionLogLevel { get; set; }

        private static MethodBase GetCallingMethod()
        {
            return new StackTrace().GetFrame( 2 ).GetMethod();
        }

        #region Log4NetWrapper implementation of IDisposable

        private class Log4NetWrapper : IExceptionLogger
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
                bool exceptionThrown = Marshal.GetExceptionCode() != 0 || Marshal.GetExceptionPointers() != IntPtr.Zero;

                _log.Logger.Log( _callingMethod.DeclaringType, MethodLogLevel,
                                 String.Format( MethodLogFormatString, "Exiting" + (exceptionThrown ? "with exception:" : ""),
                                                exitingMethod.Key,
                                                exitingMethod.Value ),
                                 null );
            }

            public void LogException( Exception exception )
            {
                var exceptionMethod = MethodStack.Peek();
                _log.Logger.Log( _callingMethod.DeclaringType, ExceptionLogLevel,
                                 String.Format( ExceptionLogFormatString, exceptionMethod.Key, exceptionMethod.Value ),
                                 exception );
            }

            #endregion
        }

        #endregion
    }
}