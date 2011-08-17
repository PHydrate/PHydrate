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
    /// Extensions on generic types
    /// </summary>
    public static class GenericExtensions
    {
        private static readonly IDictionary< Type, PropertyInfo[] > PropertyInfoCache =
            new Dictionary< Type, PropertyInfo[] >();

        /// <summary>
        /// Gets the data parameters from an instance.
        /// </summary>
        /// <param name="instance">The type.</param>
        /// <param name="parameterPrefix">The parameter prefix.</param>
        /// <returns></returns>
        [ NotNull ]
        [ SuppressMessage( "Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures" ) ]
        public static IEnumerable< KeyValuePair< string, object > > GetDataParameters< T >( this T instance,
                                                                                            string parameterPrefix )
        {
            if ( !PropertyInfoCache.ContainsKey( typeof(T) ) )
                PropertyInfoCache.Add( typeof(T),
                                       typeof(T).GetProperties( BindingFlags.Instance | BindingFlags.Public ) );

            PropertyInfo[] properties = PropertyInfoCache[ typeof(T) ];

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
        [ SuppressMessage( "Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter" ) ]
        public static void SetPropertyValueWithAttribute< TInstance, TAttributeType >( this TInstance obj, object value )
            where TAttributeType : Attribute
        {
            IMemberInfo member = typeof(TInstance).GetMembersWithAttribute< TAttributeType >().FirstOrDefault();

            if ( member == null )
                throw new PHydrateException( "Member with attribute {0} not found in type {1}",
                                             typeof(TAttributeType).Name, typeof(TInstance).Name );

            member.SetValue( obj, value );
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static void SetPropertyValue<TInstance>(this object obj, string propertyName, object value)
        {
            MemberInfo member =
                typeof(TInstance).GetMember( propertyName, MemberTypes.Property | MemberTypes.Field,
                                             BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ).
                    FirstOrDefault();

            if (member == null)
                throw new PHydrateException( "Member with name {0} not found in type {1}", propertyName,
                                             typeof(TInstance).Name );

            member.CreateWrapper().SetValue( obj, value );
        }

        /// <summary>
        /// Gets the property values with attribute.
        /// </summary>
        /// <typeparam name="TAttributeType">The type of the attribute to grab property values from.</typeparam>
        /// <param name="obj">The object to work on.</param>
        /// <returns></returns>
        [ NotNull ]
        [ SuppressMessage( "Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter" ) ]
        [ SuppressMessage( "Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj" ) ]
        public static IEnumerable< object > GetPropertyValuesWithAttribute< TAttributeType >( this object obj )
            where TAttributeType : Attribute
        {
            return
                obj.GetType().GetMembersWithAttribute< TAttributeType >().Select(
                    member =>
                    member.GetValue( obj ) );
        }

        /// <summary>
        /// Gets the objects hash code based on the type and the primary keys defined.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static int GetObjectsHashCodeByPrimaryKeys( this object item )
        {
            IEnumerable< object > primaryKeyFields = item.GetPropertyValuesWithAttribute< PrimaryKeyAttribute >();
            if ( primaryKeyFields.Count() == 0 )
                primaryKeyFields = new List< object > { item };

            return item.GetType().GetObjectsHashCodeByFieldValues( primaryKeyFields );
        }

        /// <summary>
        /// Gets the objects hash code based on arbritary field values.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static int GetObjectsHashCodeByFieldValues( this Type type, IEnumerable< object > values )
        {
            unchecked
            {
                int hash = 73;

                // Throw the type of the object into the hash, to prevent collisions
                hash = hash * 137 + type.GetHashCode();

                return values.Aggregate( hash,
                                         ( current, primaryKeyField ) => current * 137 + primaryKeyField.GetHashCode() );
            }
        }

        /// <summary>
        /// Returns null if the object is DbNull, else returns the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        [ SuppressMessage( "Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj" ) ]
        public static T DBNullToDefault< T >( this object obj )
        {
            return obj is DBNull ? default( T ) : (T)obj;
        }

        /// <summary>
        /// Executes the generic method.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="methodCall">The method call.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="PHydrateInternalException">Lambda does not contain a method call.</exception>
        [ SuppressMessage( "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters" ) ]
        [ SuppressMessage( "Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures" ) ]
        public static object ExecuteGenericMethod< TSource >( this TSource obj,
                                                              Expression< Func< TSource, object > > methodCall,
                                                              Type type )
        {
            var unaryExpression = methodCall.Body as UnaryExpression;
            MethodCallExpression method;
            if ( unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert )
                method = unaryExpression.Operand as MethodCallExpression;
            else
                method = methodCall.Body as MethodCallExpression;

            if ( method == null )
                throw new PHydrateInternalException( "Lambda does not contain a method call." );

            MethodInfo methodInfo = typeof(TSource).GetMethod( method.Method.Name,
                                                               BindingFlags.Instance | BindingFlags.Static |
                                                               BindingFlags.Public |
                                                               BindingFlags.NonPublic );

            MethodInfo genericMethod = methodInfo.MakeGenericMethod( type );

            return genericMethod.Invoke( obj, method.Arguments.Select( x => x.GetValue() ).ToArray() );
        }
    }
}