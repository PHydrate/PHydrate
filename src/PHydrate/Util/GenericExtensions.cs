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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PHydrate.Attributes;
using PHydrate.Util.MemberInfoWrapper;

namespace PHydrate.Util
{
    /// <summary>
    /// Extensions on generic types
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Gets the data parameters from an instance.
        /// </summary>
        /// <param name="instance">The type.</param>
        /// <param name="parameterPrefix">The parameter prefix.</param>
        /// <returns></returns>
        [ NotNull ]
        public static IEnumerable< KeyValuePair< string, object > > GetDataParameters< T >( this T instance,
                                                                                            string parameterPrefix )
        {
            PropertyInfo[] properties = typeof(T).GetProperties( BindingFlags.Instance | BindingFlags.Public );

            return properties.Select( property => new KeyValuePair< string, object >( parameterPrefix + property.Name,
                                                                                      property.GetValue( instance, null ) ) );
        }

        /// <summary>
        /// Sets the property with attribute.
        /// </summary>
        /// <typeparam name="TInstance">The type of the instance.</typeparam>
        /// <typeparam name="TAttributeType">The type of the attribute.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="PHydrateException">Thrown if the member is not settable.</exception>
        public static void SetPropertyValueWithAttribute< TInstance, TAttributeType >( this TInstance obj, object value )
            where TAttributeType : Attribute
        {
            IMemberInfo member = typeof(TInstance).GetMembersWithAttribute< TAttributeType >().FirstOrDefault();

            if ( member == null )
                throw new PHydrateException( "Member with attribute {0} not found in type {1}",
                                             typeof(TAttributeType).Name, typeof(TInstance).Name );

            member.SetValue( obj, value );
        }

        /// <summary>
        /// Gets the property values with attribute.
        /// </summary>
        /// <typeparam name="TAttributeType">The type of the attribute to grab property values from.</typeparam>
        /// <param name="obj">The object to work on.</param>
        /// <returns></returns>
        public static IEnumerable<object> GetPropertyValuesWithAttribute<TAttributeType>(this object obj)
            where TAttributeType : Attribute
        {
            return
                obj.GetType().GetMembersWithAttribute< TAttributeType >().Select(
                    member =>
                    member.GetValue( obj, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ) );
        }
    }
}