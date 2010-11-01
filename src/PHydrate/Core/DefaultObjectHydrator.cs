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

namespace PHydrate.Core
{
    /// <summary>
    ///   Default hydrator
    /// </summary>
    internal class DefaultObjectHydrator : IObjectHydrator
    {
        #region IObjectHydrator Members

        public T Hydrate< T >( IDictionary< string, object > columnValues )
        {
            // Find a suitable constructor
            var objToHydrate = GetObject< T >( columnValues );

            // Go through all the properties and get them from the dictionary argument
            PropertyInfo[] propertySetters = typeof(T).GetProperties( BindingFlags.Instance | BindingFlags.Public );
            foreach ( PropertyInfo pi in propertySetters.Where( pi => columnValues.ContainsKey( pi.Name ) ) )
                pi.SetValue( objToHydrate, columnValues[ pi.Name ], BindingFlags.Public | BindingFlags.NonPublic, null,
                             null, null );

            return objToHydrate;
        }

        #endregion

        private static T GetObject< T >( IDictionary< string, object > columnValues )
        {
            // Try to get a default constructor
            ConstructorInfo defaultConstructor = typeof(T).GetConstructor( Type.EmptyTypes );

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if ( defaultConstructor != null )
                return (T)defaultConstructor.Invoke( new object[] {} );
            // ReSharper restore ConditionIsAlwaysTrueOrFalse

            // ReSharper disable HeuristicUnreachableCode
            ConstructorInfo[] otherConstructors = typeof(T).GetConstructors();
            foreach ( ConstructorInfo ci in otherConstructors )
            {
                var arguments = new List< object >();
                ParameterInfo[] parameters = ci.GetParameters();
                foreach ( ParameterInfo pi in parameters )
                {
                    string actualName;
                    if ( !columnValues.ContainsKeyNoCase( pi.Name, out actualName ) )
                        break;
                    arguments.Add( columnValues[ actualName ] );
                }
                if ( arguments.Count == parameters.Length )
                    return (T)ci.Invoke( arguments.ToArray() );
            }
            throw new PHydrateException( String.Format( "Could not find constructor for hydration of object {0}",
                                                        typeof(T).FullName ) );
            // ReSharper restore HeuristicUnreachableCode
        }
    }
}