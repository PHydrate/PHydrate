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

using System;
using System.Reflection;

namespace PHydrate.Util.MemberInfoWrapper
{
    /// <summary>
    /// Default wrapper for MemberInfos that have no Getters or Setters
    /// </summary>
    public class MemberInfoWrapper : IMemberInfo
    {
        /// <summary>
        /// The internal MemberInfo being wrapped.
        /// </summary>
        protected readonly MemberInfo MemberInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInfoWrapper"/> class.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        public MemberInfoWrapper( MemberInfo memberInfo )
        {
            MemberInfo = memberInfo;
        }

        #region Implementation of IMemberInfo

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public virtual object GetValue( object obj )
        {
            throw new PHydrateInternalException(
                "Cannot get value from member {0}, because it is not a field or property.", MemberInfo.Name );
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public virtual void SetValue( object obj, object value )
        {
            throw new PHydrateInternalException(
                "Cannot set value on member {0}, because it is not a field or property.", MemberInfo.Name );
        }

        /// <summary>
        /// Gets the wrapped MemberInfo.
        /// </summary>
        public virtual MemberInfo Wrapped
        {
            get { return MemberInfo; }
        }

        /// <summary>
        /// Gets the type of the member.
        /// </summary>
        public virtual Type Type
        {
            get { throw new PHydrateInternalException( "Could not get type from generic MemberInfo" ); }
        }

        #endregion
    }
}