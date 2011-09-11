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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
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
        [ CanBeNull ]
        public static T GetAttribute< T >( this Type type ) where T : Attribute
        {
            if ( !AttributeCache.ContainsKey( type ) )
                AttributeCache.Add( type, type.GetCustomAttributes( true ) );

            return AttributeCache[ type ].Where( x => x.GetType() == typeof(T) ).FirstOrDefault() as T;
        }

        /// <summary>
        /// Gets the default constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The default constructor for the type, or null if it does not exist.</returns>
        [ CanBeNull ]
        public static ConstructorInfo GetDefaultConstructor( this Type type )
        {
            if ( !ConstructorCache.ContainsKey( type ) )
                ConstructorCache.Add( type, type.GetConstructor( Type.EmptyTypes ) );
            return ConstructorCache[ type ];
        }

        /// <summary>
        /// Constructs the specified type.
        /// </summary>
        /// <typeparam name="T">The type to cast to on return.</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>The constructed type.</returns>
        /// <exception cref="PHydrateException">Unable to construct object {0}, no default constructor.</exception>
        [ NotNull ]
        public static T ConstructUsingDefaultConstructor< T >( this Type type )
        {
            ConstructorInfo defaultConstructor = type.GetDefaultConstructor();
            if ( defaultConstructor == null )
                throw new PHydrateException( "Unable to construct object {0}, no default constructor.", type.Name );

            return (T)defaultConstructor.Invoke( new object[] { } );
        }

        /// <summary>
        /// Gets the members with attribute.
        /// </summary>
        /// <typeparam name="T">The attribute</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>An enumerable of all members of the type that have the specified attribute.</returns>
        [ NotNull ]
        [ SuppressMessage( "Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter" ) ]
        public static IEnumerable< IMemberInfo > GetMembersWithAttribute< T >( this Type type ) where T : Attribute
        {
            if ( !MemberCache.ContainsKey( type ) )
                MemberCache.Add( type,
                                 type.GetMembers( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ) );

            return
                MemberCache[ type ].Where( x => x.GetCustomAttributes( typeof(T), true ).Length > 0 ).Select(
                    x => x.CreateWrapper() );
        }

        /// <summary>
        /// Executes a method on a generic type.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TReturn">The type of the return value.</typeparam>
        /// <param name="genericType">The generic type parameter to use when constructing the concrete instance.</param>
        /// <param name="methodCall">The method call to make, including arguments.</param>
        /// <param name="constructorParameters">The constructor parameters.</param>
        /// <returns>The return value from the method to be called.</returns>
        /// <exception cref="PHydrateInternalException">Lambda does not contain a method call.</exception>
        [ SuppressMessage( "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters" ) ]
        [ SuppressMessage( "Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures" ) ]
        public static TReturn ExecuteGenericMethod< TSource, TReturn >( this Type genericType,
                                                                        Expression< Func< TSource, TReturn > >
                                                                            methodCall,
                                                                        params object[] constructorParameters )
        {
            var method = methodCall.Body as MethodCallExpression;
            if ( method == null )
                throw new PHydrateInternalException( "Lambda does not contain a method call." );

            return genericType.ExecuteGenericMethod< TSource, TReturn >( method.Method.Name, constructorParameters,
                                                                         method.Arguments.Select( x => x.GetValue() ).
                                                                             ToArray() );
        }

        /// <exception cref="PHydrateInternalException">Type {0} is not a generic type</exception>
        private static TReturn ExecuteGenericMethod< TSource, TReturn >( this Type genericType, string methodName,
                                                                         object[] constructorParameters,
                                                                         object[] methodParameters )
        {
            Type type = typeof(TSource);

            if ( !type.IsGenericType && !type.IsGenericTypeDefinition )
                throw new PHydrateInternalException( "Type {0} is not a generic type", type.Name );

            Type genericTypeDefinition = type.GetGenericTypeDefinition();

            Type innerClass = genericTypeDefinition.MakeGenericType( genericType );
            object c = innerClass.ConstructObjectUsingConstructorMatchingParameters( constructorParameters );
            var method = innerClass.GetMethod( methodName );
            return ( (TReturn)method.Invoke( c, methodParameters ) );
        }

        private static object ConstructObjectUsingConstructorMatchingParameters( this Type innerClass,
                                                                                 params object[] constructorParameters )
        {
            ConstructorInfo[] constructors = innerClass.GetConstructors();
            ConstructorInfo constructor =
                constructors.Where( ci => ci.MatchesParameters( constructorParameters ) ).FirstOrDefault();

            if (constructor == null)
                throw new PHydrateException( "Could not find matching constructor" );

            return constructor.Invoke( constructorParameters );
        }

        /// <summary>
        /// Gets IMemberInfos for a type by name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="memberNames">The member names.</param>
        /// <returns></returns>
        [ NotNull ]
        public static IEnumerable< IMemberInfo > GetMembersByName( this Type type, params string[] memberNames )
        {
            return
                memberNames.Select( x => type.GetMember( x ) ).Where( memberInfos => memberInfos.Length != 0 ).Select(
                    memberInfos => memberInfos[ 0 ].CreateWrapper() );
        }

        /*
        /// <summary>
        /// Returns a boolean value indicating that the type inherits from or implements the base type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="baseType">Type of the base.</param>
        /// <returns></returns>
        public static bool InheritsFromOrImplements( this Type type, Type baseType )
        {
            if ( type == baseType )
                return true;

            if ( baseType.IsInterface )
                return type.GetInterfaces().Where( x => x.Name == baseType.Name ).Any();

            while ( type.BaseType != null )
            {
                if ( type.BaseType == baseType )
                    return true;
                type = type.BaseType;
            }
            return false;
        }
*/

        /// <summary>
        /// Gets the settable members (properties and fields).
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        [ NotNull ]
        public static IEnumerable< IMemberInfo > GetSettableMembers( this Type type )
        {
            return
                type.GetProperties( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ).Select(
                    x => x.CreateWrapper() ).Concat(
                        type.GetFields( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ).Select(
                            x => x.CreateWrapper() ) );
        }
    }
}