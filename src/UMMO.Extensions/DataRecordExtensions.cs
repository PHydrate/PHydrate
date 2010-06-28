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
    public static class DataRecordExtensions
    {
        public static T Value< T >( this IDataRecord dataRecord, string columnName )
        {
            return GetValue< T >( dataRecord[ columnName ] );
        }

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