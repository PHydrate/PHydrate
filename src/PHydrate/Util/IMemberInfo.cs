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
using System.Reflection;

namespace PHydrate.Util
{
    /// <summary>
    /// Wrapper for MemberInfo derivatives
    /// </summary>
    public interface IMemberInfo
    {
        /// <summary>
        /// Gets the wrapped MemberInfo.
        /// </summary>
        MemberInfo Wrapped { get; }

        /// <summary>
        /// Gets the value from a specific object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        [ SuppressMessage( "Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj" ) ]
        object GetValue( object obj );

        /// <summary>
        /// Sets a value on a specific object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        [ SuppressMessage( "Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj" ) ]
        void SetValue( object obj, object value );

        /// <summary>
        /// Gets the type of the member.
        /// </summary>
        Type Type { get; }
    }
}