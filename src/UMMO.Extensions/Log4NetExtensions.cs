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
using log4net;
using log4net.Core;

namespace UMMO.Extensions
{
    public static class Log4NetExtensions
    {
        private static readonly Stack< string > MethodStack = new Stack< string >();

        static Log4NetExtensions()
        {
            // Set the default log level for method wrappers.
            MethodLogLevel = Level.Debug;

            // Default string to log method entry/exit
            // TODO: Define this string and it's format parameters!
            MethodLogFormatString = "";
        }

        public static IDisposable LogMethod(this ILog log)
        {
            MethodBase callingMethod = GetCallingMethod();
            MethodStack.Push( callingMethod.Name );

            log.Logger.Log( callingMethod.DeclaringType, MethodLogLevel, String.Format(MethodLogFormatString), null );

            return new Log4NetWrapper( log, callingMethod );
        }

        public static string MethodLogFormatString { get; set; }

        public static Level MethodLogLevel { get; set; }

        private static MethodBase GetCallingMethod()
        {
            return new StackTrace().GetFrame( 2 ).GetMethod();
        }

        #region Log4NetWrapper implementation of IDisposable

        public class Log4NetWrapper : IDisposable
        {
            private readonly ILog _log;
            private readonly MethodBase _callingMethod;

            internal Log4NetWrapper( ILog log, MethodBase callingMethod )
            {
                _log = log;
                _callingMethod = callingMethod;
            }

            private static bool ExceptionWasThrown()
            {
                return Marshal.GetExceptionCode() != 0;
            }

            #region Implementation of IDisposable

            public void Dispose()
            {
                _log.Logger.Log( _callingMethod.DeclaringType, MethodLogLevel, String.Format( MethodLogFormatString ),
                                 null );

                MethodStack.Pop();
            }

            #endregion
        }

        #endregion
    }
}