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
using System.Linq;
using System.Reflection;
using PHydrate.Attributes;
using PHydrate.Util;

namespace PHydrate.Core
{
    /// <summary>
    ///   Default hydrator
    /// </summary>
    internal class DefaultObjectHydrator : IDefaultObjectHydrator
    {
        #region IObjectHydrator Members

        /// <summary>
        /// Hydrates the specified object type.
        /// </summary>
        /// <typeparam name="T">The type of object to hydrate</typeparam>
        /// <param name="columnValues">The column values.</param>
        /// <returns>
        /// The hydrated object
        /// </returns>
        /// <exception cref="PHydrateException">Unable to populate Primary Key values for {0}</exception>
        public T Hydrate< T >( IDictionary< string, object > columnValues )
        {
            // Find a suitable constructor
            var objToHydrate = GetObject< T >( columnValues );

            if (typeof(T).IsClass)
            {
                // Go through all the properties and get them from the dictionary argument
                IEnumerable< IMemberInfo > propertySetters = typeof(T).GetSettableMembers();
                PopulateObjectProperties( objToHydrate, columnValues, propertySetters );
                PopulateInnerObjects( objToHydrate, columnValues, propertySetters );
            }
            return objToHydrate;
        }

        /// <exception cref="PHydrateException">Unable to populate Primary Key values for {0}</exception>
        private void PopulateInnerObjects< T >( T objToHydrate, IDictionary< string, object > columnValues,
                                                IEnumerable< IMemberInfo > propertySetters )
        {
            foreach (
                IMemberInfo mi in
                    propertySetters.Where( x => x.Type.GetMembersWithAttribute< PrimaryKeyAttribute >().Count() > 0 ) )
            {
                // Add in an empty copy of the object, populate the [PrimaryKey] if possible.
                object innerObject = this.ExecuteGenericMethod( x => GetObject< object >( columnValues ), mi.Type );
                foreach ( IMemberInfo primaryKey in mi.Type.GetMembersWithAttribute< PrimaryKeyAttribute >() )
                {
                    if ( !columnValues.ContainsKey( primaryKey.Wrapped.Name ) )
                        throw new PHydrateException( "Unable to populate Primary Key values for {0}", mi.Wrapped.Name );

                    primaryKey.SetValue( innerObject,
                                         columnValues[ primaryKey.Wrapped.Name ].DBNullToDefault< object >() );
                }
                mi.SetValue( objToHydrate, innerObject );
            }
        }

        private static void PopulateObjectProperties< T >( T objToHydrate, IDictionary< string, object > columnValues,
                                                           IEnumerable< IMemberInfo > propertySetters )
        {
            foreach ( IMemberInfo  pi in propertySetters.Where( pi => columnValues.ContainsKey( pi.Wrapped.Name ) ) )
                pi.SetValue( objToHydrate, columnValues[ pi.Wrapped.Name ].DBNullToDefault< object >() );
        }

        #endregion

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnValues">The column values.</param>
        /// <returns></returns>
        /// <exception cref="PHydrateException">Could not find constructor for hydration of object {0}</exception>
        private static T GetObject< T >( IDictionary< string, object > columnValues )
        {
            // Try to get a default constructor
            ConstructorInfo defaultConstructor = typeof(T).IsClass ? typeof(T).GetDefaultConstructor() : null;

            if ( defaultConstructor != null )
                return (T)defaultConstructor.Invoke( new object[] { } );

            ConstructorInfo[] otherConstructors = typeof(T).GetConstructors();
            foreach ( ConstructorInfo ci in otherConstructors )
            {
                ParameterInfo[] parameters = ci.GetParameters();
                var arguments =
                    parameters.TakeWhile( pi => columnValues.ContainsKey( pi.Name ) ).Select(
                        pi => columnValues[ pi.Name ] ).ToList();
                if ( arguments.Count == parameters.Length )
                    return (T)ci.Invoke( arguments.ToArray() );
            }

            if ( typeof(T).IsValueType )
                return GetStruct< T >( columnValues );
            throw new PHydrateException( "Could not find constructor for hydration of object {0}",
                                         typeof(T).FullName );
        }

        private static T GetStruct< T >( IEnumerable< KeyValuePair< string, object > > columnValues )
        {
            if (!typeof(T).IsValueType)
                throw new PHydrateInternalException( "Attempt to call GetStruct for non-value type." );

            object newStruct = Activator.CreateInstance< T >();

            foreach (KeyValuePair<string, object> columnValue in columnValues)
                newStruct.SetPropertyValue<T>( columnValue.Key, columnValue.Value );

            return (T)newStruct;
        }
    }
}