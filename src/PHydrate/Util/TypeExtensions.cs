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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PHydrate.Attributes;
using PHydrate.Util.MemberInfoWrapper;

namespace PHydrate.Util
{
    /// <summary>
    /// Extension methods for <see cref="System.Data"/>
    /// </summary>
    public static class TypeExtensions
    {
        private static readonly IDictionary< Type, object[] > AttributeCache = new Dictionary< Type, object[] >();

        private static readonly IDictionary< Type, ConstructorInfo > ConstructorCache =
            new Dictionary< Type, ConstructorInfo >();

        private static readonly IDictionary< Type, MemberInfo[] > MemberCache = new Dictionary< Type, MemberInfo[] >();

        /// <summary>
        /// Gets a specific attribute from a type.
        /// </summary>
        /// <typeparam name="T">The attribute to retrieve</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>The attribute, or null if not found.</returns>
        [CanBeNull]
        public static T GetAttribute< T >( this Type type ) where T : Attribute
        {
            if (!AttributeCache.ContainsKey(type))
                AttributeCache.Add( type, type.GetCustomAttributes( true ) );

            return AttributeCache[ type ].Where(x => x.GetType() == typeof(T)  ).FirstOrDefault() as T;
        }

        /// <summary>
        /// Gets the default constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The default constructor for the type, or null if it does not exist.</returns>
        [CanBeNull]
        public static ConstructorInfo GetDefaultConstructor(this Type type)
        {
            if (!ConstructorCache.ContainsKey(type))
                ConstructorCache.Add( type, type.GetConstructor( Type.EmptyTypes ) );
            return ConstructorCache[ type ];
        }

        /// <summary>
        /// Constructs the specified type.
        /// </summary>
        /// <typeparam name="T">The type to cast to on return.</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>The constructed type.</returns>
        [NotNull]
        public static T ConstructUsingDefaultConstructor<T>(this Type type)
        {
            ConstructorInfo defaultConstructor = type.GetDefaultConstructor();
            if (defaultConstructor == null)
                throw new PHydrateInternalException(
                    String.Format( "Unable to construct object {0}, no default constructor.", typeof(T).Name ) );

            return (T)defaultConstructor.Invoke( new object[] {} );
        }

        /// <summary>
        /// Gets the members with attribute.
        /// </summary>
        /// <typeparam name="T">The attribute</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>An enumerable of all members of the type that have the specified attribute.</returns>
        [NotNull]
        public static IEnumerable<IMemberInfo> GetMembersWithAttribute<T>(this Type type) where T : Attribute
        {
            if (!MemberCache.ContainsKey(type))
                MemberCache.Add( type,
                                 type.GetMembers( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ) );

            return
                MemberCache[ type ].Where( x => x.GetCustomAttributes( typeof(T), true ).Length > 0 ).Select(
                    x => x.CreateWrapper() );
        }
    }
}