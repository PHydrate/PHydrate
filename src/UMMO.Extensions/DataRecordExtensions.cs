#region Copyright

// This file is part of UMMO.
// 
// UMMO is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// UMMO is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with UMMO.  If not, see <http://www.gnu.org/licenses/>.
//  
// Copyright 2010, Stephen Michael Czetty

#endregion

using System;
using System.Data;

namespace UMMO.Extensions
{
    /// <summary>
    /// Extensions on classes that implement IDataRecord
    /// </summary>
    public static class DataRecordExtensions
    {
        /// <summary>
        /// Returns the value found in the column, or the default of T if the column is null (or DBNull).
        /// This has the effect of transforming all DBNulls to CLR-native null values, so long as <typeparamref name="T"/>
        /// is a nullable type.
        /// </summary>
        /// <typeparam name="T">The type expected to be found in the column</typeparam>
        /// <param name="dataRecord">The data record.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The value found in the column, null, or (for value types) the default value for <typeparamref name="T"/></returns>
        public static T Value< T >( this IDataRecord dataRecord, string columnName )
        {
            return GetValue< T >( dataRecord[ columnName ] );
        }

        /// <summary>
        /// Returns the value found in the column, or the default of T if the column is null (or DBNull).
        /// This has the effect of transforming all DBNulls to CLR-native null values, so long as <typeparamref name="T"/>
        /// is a nullable type.
        /// </summary>
        /// <typeparam name="T">The type expected to be found in the column</typeparam>
        /// <param name="dataRecord">The data record.</param>
        /// <param name="columnOrdinal">The column ordinal.</param>
        /// <returns>The value found in the column, null, or (for value types) the default value for <typeparamref name="T"/></returns>
        public static T Value< T >( this IDataRecord dataRecord, int columnOrdinal )
        {
            return GetValue< T >( dataRecord[ columnOrdinal ] );
        }

        private static T GetValue< T >( object value ) {
            if ( typeof(T).IsEnum )
            {
                if ( value is int )
                    return (T)value;
                return ( value is DBNull || value == null )
                           ? default( T )
                           : (T)Enum.Parse( typeof(T), value.ToString() );
            }

            return ( value is DBNull || value == null ) ? default( T ) : (T)value;
        }
    }
}