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

#endregion

using System.Reflection;

namespace PHydrate.Util.MemberInfoWrapper
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
        /// Gets the value.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="invokeAttr">The invoke attr.</param>
        /// <returns></returns>
        object GetValue( object obj, BindingFlags invokeAttr );

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        void SetValue( object obj, object value );
    }
}