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
    [ CLSCompliant( false ) ]
    public class LogAmendment< T > : Amendment< T, T >
    {
        /// <summary>
        /// Amends the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        public override void Amend( Method method )
        {
            if ( !ShouldApplyLogging( method.MethodInfo ) )
                return;

            method.Before( Logger< T >.BeginMethod );
            method.After( Logger< T >.EndMethod );
        }

        /// <summary>
        /// Amends the specified constructor.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        public override void Amend( Constructor constructor )
        {
            if ( !ShouldApplyLogging( constructor.ConstructorInfo ) )
                return;

            constructor.Before( Logger< T >.BeginMethod );
            constructor.After( Logger< T >.EndMethod );
        }

        private static bool ShouldApplyLogging( MemberInfo member )
        {
            return member.GetCustomAttributes( typeof(DoNotLogAttribute), true ).Length == 0;
        }
    }
}