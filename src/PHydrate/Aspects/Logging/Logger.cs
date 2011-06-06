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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using log4net;
using log4net.Core;
using PHydrate.Attributes;

namespace PHydrate.Aspects.Logging
{
    /// <summary>
    /// Base class for generic Logger{T} class.  Contains indention code common to all classes.
    /// </summary>
    public abstract class Logger
    {
        /// <summary>
        /// Level to log at.  Default is Debug.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        protected static readonly Level LoggingLevel = Level.Debug;

        private static int _indentLevel;
        private const int IndentSize = 4;

        /// <summary>
        /// Indents this instance.
        /// </summary>
        protected static void Indent()
        {
            _indentLevel++;
        }

        /// <summary>
        /// Unindents this instance.
        /// </summary>
        protected static void Unindent()
        {
            if ( --_indentLevel < 0 )
                _indentLevel = 0;
        }

        /// <summary>
        /// Gets a string of spaces for indention purposes
        /// </summary>
        /// <returns></returns>
        protected static string IndentionString()
        {
            return new string( ' ', _indentLevel * IndentSize );
        }
    }

    /// <summary>
    /// Class created by amendments for logging
    /// </summary>
    /// <typeparam name="T">The class being logged</typeparam>
    [ UsedImplicitly ]
    public sealed class Logger< T > : Logger
    {
        [CoverageExclude]
        private Logger() {}

        private static readonly ILog Log = LogManager.GetLogger( typeof(T) );

        /// <summary>
        /// Log the start of the method.
        /// </summary>
        /// <param name="instance">The instance of the class</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static void BeginMethod(T instance, string methodName, object[] parameters)
        {
            DoLog( "Entering method: " + methodName );
            Indent();

            if ( parameters.Length == 0 )
                return;

            int parameterCount = 0;
            DoLog( "Parameters:" );
            foreach ( object o in parameters )
            {
                string parameterType = o == null ? "<null>" : o.GetType().Name;
                DoLog( String.Format( CultureInfo.CurrentCulture, "{0}: ({1}) {2}", parameterCount++, parameterType, o ?? "<null>" ) );
            }
        }

        private static void DoLog( string stringToLog )
        {
            Log.Logger.Log( typeof(T), LoggingLevel, IndentionString() + stringToLog, null );
        }

        /// <summary>
        /// Log the end of the method.
        /// </summary>
        /// <param name="instance">The instance of the class</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">Parameters passed to the method call</param>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static void EndMethod(T instance, string methodName, object[] parameters)
        {
            Unindent();
            DoLog( "Leaving method: " + methodName );
        }
    }
}