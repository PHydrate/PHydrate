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
using System.Reflection;
using Afterthought;

namespace PHydrate.Aspects.Logging
{
    /// <summary>
    /// Add logging to all methods of a class
    /// </summary>
    /// <typeparam name="T">The type of the class</typeparam>
    // TODO: Modify Afterthought to make this testable (add interfaces around Method, Constructor, etc.) -- New fluent configuration may preclude this
    [ CLSCompliant( false ) ]
    [CoverageExclude]
    public sealed class LogAmendment< T > : Amendment< T, T >
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogAmendment&lt;T&gt;"/> class.
        /// </summary>
        public LogAmendment()
        {
            Methods
                .Where( x => ShouldApplyLogging( x.MethodInfo ) )
                .Before( Logger< T >.BeginMethod )
                .After( Logger< T >.EndMethod );
                //.Catch<Exception>( (MethodEnumeration.CatchMethodAction< Exception >)Logger< T >.LogException ); // TODO: Afterthought chokes on this.

            Constructors
                .Where( x => ShouldApplyLogging( x.ConstructorInfo ) )
                .Before( Logger< T >.BeginMethod )
                .After( Logger< T >.EndMethod );
        }

        private static bool ShouldApplyLogging( ICustomAttributeProvider member )
        {
            return member.GetCustomAttributes( typeof(DoNotLogAttribute), true ).Length == 0;
        }
    }
}