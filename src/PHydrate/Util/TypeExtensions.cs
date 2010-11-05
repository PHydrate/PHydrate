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
// Copyright 2010, Stephen Michael Czetty
// 

#endregion

using System;

namespace PHydrate.Util
{
    /// <summary>
    /// Extension methods for <see cref="System.Data"/>
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets a specific attribute from a type.
        /// </summary>
        /// <typeparam name="T">The attribute to retrieve</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>The attribute, or null if not found.</returns>
        public static T GetAttribute< T >( this Type type ) where T : Attribute
        {
            object[] attributes = type.GetCustomAttributes( typeof(T), true );
            if ( attributes.Length > 0 )
                return attributes[ 0 ] as T;
            return null;
        }
    }
}