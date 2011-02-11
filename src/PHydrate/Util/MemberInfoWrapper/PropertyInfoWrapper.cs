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
    /// Wrapper for PropertyInfo types
    /// </summary>
    internal class PropertyInfoWrapper : MemberInfoWrapper
    {
        public PropertyInfoWrapper( MemberInfo propertyInfo ) : base( propertyInfo ) {}

        public override object GetValue( object obj, BindingFlags invokeAttr )
        {
            return ( (PropertyInfo)MemberInfo ).GetValue( obj, invokeAttr, null, null, null );
        }

        public override void SetValue( object obj, object value )
        {
            ( (PropertyInfo)MemberInfo ).SetValue( obj, value, null );
        }

        public override Type Type
        {
            get { return ( (PropertyInfo)MemberInfo ).PropertyType; }
        }
    }
}