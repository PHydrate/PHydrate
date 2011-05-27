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
using System.Data;

namespace PHydrate.Util
{
    /// <summary>
    /// Extension methods on IDataReader.
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Return an IDictionary of the current IDataRecord
        /// </summary>
        /// <param name="dataRecord">The data reader.</param>
        /// <returns>The record translated as an IDictionary</returns>
        public static IDictionary< string, object > ToDictionary( this IDataRecord dataRecord )
        {
            var dictionary = new Dictionary< string, object >( StringComparer.OrdinalIgnoreCase );
            for ( int i = 0; i < dataRecord.FieldCount; i++ )
                dictionary.Add( dataRecord.GetName( i ), dataRecord.GetValue( i ) );

            return dictionary;
        }
    }
}