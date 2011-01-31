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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UMMO.TestingUtils
{
    /// <summary>
    /// Implementation of IDataReader, intended for testing ADO-based code.
    /// </summary>
    public class DataReaderMock : IDataReader
    {
        private readonly IList< IList< string > > _recordSetColumnNames;
        private readonly IList< IList< IList< object > > > _recordsToRetrieve;
        private bool _isClosed;
        private bool _readyForPlayback;
        private int _recordsetNumber;
        private int _rowNumber;
        private DataTable _schemaTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataReaderMock"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is obsolete, is here for backward compatibility.  You should use the
        /// parameterless constructor and AddRecordSet and AddRow methods.
        /// </remarks>
        /// <param name="recordsToRetrieve">A list of KeyValuePairs defining the column names and values of each record to retrieve.</param>
        [ Obsolete( "Use the parameterless constructor and AddRecordSet/AddRow methods." ) ]
        public DataReaderMock( params IList< KeyValuePair< string, object > >[] recordsToRetrieve )
        {
            _recordsToRetrieve = new List< IList< IList< object > > >();
            _recordSetColumnNames = new List< IList< string > >();
            _recordsetNumber = 0;
            _rowNumber = -1;

            foreach ( IList< KeyValuePair< string, object > > record in recordsToRetrieve )
                AddRecordSet( record.Select( x => x.Key ).ToArray() )
                    .AddRow( record.Select( x => x.Value ).ToArray() );

            Playback();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataReaderMock"/> class.
        /// </summary>
        public DataReaderMock()
        {
            _recordsToRetrieve = new List< IList< IList< object > > >();
            _recordSetColumnNames = new List< IList< string > >();
            _recordsetNumber = 0;
            _rowNumber = -1;
            _readyForPlayback = false;
        }

        #region IDataReader Members

        /// <summary>
        /// Marks the datareader as closed
        /// </summary>
        public void Dispose()
        {
            ThrowUnlessInPlaybackMode();
            IsClosed = true;
        }

        /// <summary>
        /// Gets the name for the field to find.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The name of the field or the empty string (""), if there is no value to return.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public string GetName( int i )
        {
            ThrowUnlessInPlaybackMode();

            return _recordSetColumnNames[ _recordsetNumber ][ i ];
        }

        /// <summary>
        /// Gets the data type information for the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The data type information for the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public string GetDataTypeName( int i )
        {
            ThrowUnlessInPlaybackMode();

            return _recordsToRetrieve[ _recordsetNumber ][ 0 ][ i ].GetType().Name;
        }

        /// <summary>
        /// Gets the <see cref="T:System.Type"/> information corresponding to the type of <see cref="T:System.Object"/> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)"/>.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The <see cref="T:System.Type"/> information corresponding to the type of <see cref="T:System.Object"/> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)"/>.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public Type GetFieldType( int i )
        {
            ThrowUnlessInPlaybackMode();

            return _recordsToRetrieve[ _recordsetNumber ][ 0 ][ i ].GetType();
        }

        /// <summary>
        /// Return the value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The <see cref="T:System.Object"/> which will contain the field value upon return.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public object GetValue( int i )
        {
            ThrowUnlessInPlaybackMode();

            return GetColumnValue( i );
        }

        /// <summary>
        /// Gets all the attribute fields in the collection for the current record.
        /// </summary>
        /// <param name="values">An array of <see cref="T:System.Object"/> to copy the attribute fields into.</param>
        /// <returns>
        /// The number of instances of <see cref="T:System.Object"/> in the array.
        /// </returns>
        public int GetValues( object[] values )
        {
            ThrowUnlessInPlaybackMode();

            for ( int i = 0; i < values.Length; i++ )
            {
                if ( i >= _recordSetColumnNames[ _recordsetNumber ].Count )
                    break;

                values[ i ] = GetColumnValue( i );
            }

            return Math.Min( values.Length, _recordSetColumnNames[ _recordsetNumber ].Count );
        }

        /// <summary>
        /// Return the index of the named field.
        /// </summary>
        /// <param name="name">The name of the field to find.</param>
        /// <returns>The index of the named field.</returns>
        public int GetOrdinal( string name )
        {
            ThrowUnlessInPlaybackMode();

            for ( int i = 0; i < _recordSetColumnNames[ _recordsetNumber ].Count; i++ )
                if ( _recordSetColumnNames[ _recordsetNumber ][ i ] == name )
                    return i;

            throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Gets the value of the specified column as a Boolean.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public bool GetBoolean( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (bool)GetColumnValue( i );
        }

        /// <summary>
        /// Gets the 8-bit unsigned integer value of the specified column.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>
        /// The 8-bit unsigned integer value of the specified column.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public byte GetByte( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (byte)GetColumnValue( i );
        }

        /// <summary>
        /// Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer offset.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <param name="fieldOffset">The index within the field from which to start the read operation.</param>
        /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
        /// <param name="bufferoffset">The index for <paramref name="buffer"/> to start the read operation.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public long GetBytes( int i, long fieldOffset, byte[] buffer, int bufferoffset, int length )
        {
            ThrowUnlessInPlaybackMode();

            if ( fieldOffset < 0 )
                return 0;

            byte[] str = Encoding.Default.GetBytes( (string)GetColumnValue( i ) );
            long k = 0;
            for ( long j = fieldOffset; j < str.Length && k < length; j++, k++ )
                buffer[ bufferoffset++ ] = str[ j ];

            return k;
        }

        /// <summary>
        /// Gets the character value of the specified column.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>
        /// The character value of the specified column.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public char GetChar( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (char)GetColumnValue( i );
        }

        /// <summary>
        /// Reads a stream of characters from the specified column offset into the buffer as an array, starting at the given buffer offset.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <param name="fieldoffset">The index within the row from which to start the read operation.</param>
        /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
        /// <param name="bufferoffset">The index for <paramref name="buffer"/> to start the read operation.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The actual number of characters read.</returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public long GetChars( int i, long fieldoffset, char[] buffer, int bufferoffset, int length )
        {
            ThrowUnlessInPlaybackMode();

            if ( fieldoffset < 0 )
                return 0;

            char[] str = ( (string)GetColumnValue( i ) ).ToCharArray();
            long k = 0;
            for ( long j = fieldoffset; j < str.Length && k < length; j++,k++ )
                buffer[ bufferoffset++ ] = str[ j ];

            return k;
        }

        /// <summary>
        /// Returns the GUID value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>The GUID value of the specified field.</returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public Guid GetGuid( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (Guid)GetColumnValue( i );
        }

        /// <summary>
        /// Gets the 16-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The 16-bit signed integer value of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public short GetInt16( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (short)GetColumnValue( i );
        }

        /// <summary>
        /// Gets the 32-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The 32-bit signed integer value of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public int GetInt32( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (int)GetColumnValue( i );
        }

        /// <summary>
        /// Gets the 64-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The 64-bit signed integer value of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public long GetInt64( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (long)GetColumnValue( i );
        }

        /// <summary>
        /// Gets the single-precision floating point number of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The single-precision floating point number of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public float GetFloat( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (float)GetColumnValue( i );
        }

        /// <summary>
        /// Gets the double-precision floating point number of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The double-precision floating point number of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public double GetDouble( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (double)GetColumnValue( i );
        }

        /// <summary>
        /// Gets the string value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>The string value of the specified field.</returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public string GetString( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (string)GetColumnValue( i );
        }

        /// <summary>
        /// Gets the fixed-position numeric value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The fixed-position numeric value of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public decimal GetDecimal( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (decimal)GetColumnValue( i );
        }

        /// <summary>
        /// Gets the date and time data value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The date and time data value of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public DateTime GetDateTime( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (DateTime)GetColumnValue( i );
        }

        /// <summary>
        /// Returns an <see cref="T:System.Data.IDataReader"/> for the specified column ordinal.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// An <see cref="T:System.Data.IDataReader"/>.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public IDataReader GetData( int i )
        {
            ThrowUnlessInPlaybackMode();

            return
                new DataReaderMock()
                    .AddRecordSet( _recordSetColumnNames[ _recordsetNumber ][ i ] )
                    .AddRow( GetColumnValue( i ) )
                    .Playback();
        }

        /// <summary>
        /// Return whether the specified field is set to null.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// true if the specified field is set to null; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public bool IsDBNull( int i )
        {
            ThrowUnlessInPlaybackMode();

            return GetColumnValue( i ) == null;
        }

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// When not positioned in a valid recordset, 0; otherwise, the number of columns in the current record. The default is -1.
        /// </returns>
        public int FieldCount
        {
            get
            {
                ThrowUnlessInPlaybackMode();
                return _recordSetColumnNames[ _recordsetNumber ].Count;
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Object"/> with the specified index.
        /// </summary>
        /// <value></value>
        object IDataRecord.this[ int i ]
        {
            get
            {
                ThrowUnlessInPlaybackMode();
                return GetColumnValue( i );
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Object"/> with the specified name.
        /// </summary>
        /// <value></value>
        object IDataRecord.this[ string name ]
        {
            get
            {
                ThrowUnlessInPlaybackMode();
                return GetColumnValue( GetOrdinal( name ) );
            }
        }

        /// <summary>
        /// Closes the <see cref="T:System.Data.IDataReader"/> Object.
        /// </summary>
        public void Close()
        {
            ThrowUnlessInPlaybackMode();

            IsClosed = true;
        }

        // TODO: Add the rest of the columns to duplicate what SqlDataReader returns
        /// <summary>
        /// Returns a <see cref="T:System.Data.DataTable"/> that describes the column metadata of the <see cref="T:System.Data.IDataReader"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Data.DataTable"/> that describes the column metadata.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        /// The <see cref="T:System.Data.IDataReader"/> is closed.
        /// </exception>
        public DataTable GetSchemaTable()
        {
            ThrowUnlessInPlaybackMode();

            if ( _schemaTable == null )
            {
                _schemaTable = new DataTable();
                _schemaTable.Columns.Add( "ColumnName" );
                _schemaTable.Columns.Add( "ColumnOrdinal", typeof(int) );
                _schemaTable.Columns.Add( "ColumnSize", typeof(int) );
                _schemaTable.Columns.Add( "DataType", typeof(Type) );

                for (int i = 0; i < _recordsToRetrieve[_recordsetNumber][0].Count; i++)
                {
                    DataRow newRow = _schemaTable.NewRow();
                    newRow[ "ColumnName" ] = _recordSetColumnNames[ _recordsetNumber ][ i ];
                    newRow[ "ColumnOrdinal" ] = i;
                    newRow[ "ColumnSize" ] = CalculateColumnSize( i );
                    newRow[ "DataType" ] = _recordsToRetrieve[ _recordsetNumber ][ 0 ][ i ].GetType();

                    _schemaTable.Rows.Add( newRow );
                }
            }

            return _schemaTable;
        }

        /// <summary>
        /// Advances the data reader to the next result, when reading the results of batch SQL statements.
        /// </summary>
        /// <returns>
        /// true if there are more rows; otherwise, false.
        /// </returns>
        public bool NextResult()
        {
            ThrowUnlessInPlaybackMode();

            _rowNumber = -1;
            _schemaTable = null;
            return ( ++_recordsetNumber < _recordsToRetrieve.Count );
        }

        /// <summary>
        /// Advances the <see cref="T:System.Data.IDataReader"/> to the next record.
        /// </summary>
        /// <returns>
        /// true if there are more rows; otherwise, false.
        /// </returns>
        public bool Read()
        {
            ThrowUnlessInPlaybackMode();

            return ++_rowNumber < _recordsToRetrieve[ _recordsetNumber ].Count;
        }

        /// <summary>
        /// Gets a value indicating the depth of nesting for the current row.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The level of nesting.
        /// </returns>
        public int Depth
        {
            get
            {
                ThrowUnlessInPlaybackMode();
                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the data reader is closed.
        /// </summary>
        /// <value></value>
        /// <returns>true if the data reader is closed; otherwise, false.
        /// </returns>
        public bool IsClosed
        {
            get
            {
                ThrowUnlessInPlaybackMode();
                return _isClosed;
            }
            private set { _isClosed = value; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.
        /// </returns>
        public int RecordsAffected
        {
            get
            {
                ThrowUnlessInPlaybackMode();

                if ( IsClosed )
                    return -1;
                return 0;
            }
        }

        #endregion

        /// <summary>
        /// Adds a record set to DataReader
        /// </summary>
        /// <param name="columnNames">The names of the columns in the recordset.</param>
        /// <returns>The current instance, for chaining calls.</returns>
        public DataReaderMock AddRecordSet( params string[] columnNames )
        {
            ThrowIfInPlaybackMode();

            _recordSetColumnNames.Add( columnNames );
            _recordsToRetrieve.Add( new List< IList< object > >() );

            return this;
        }

        /// <summary>
        /// Adds a row to the current record set
        /// </summary>
        /// <param name="columnValues">The column values.</param>
        /// <returns>The current instance, for chaining calls.</returns>
        public DataReaderMock AddRow( params object[] columnValues )
        {
            ThrowIfInPlaybackMode();

            if ( _recordsToRetrieve.Count == 0 )
                throw new InvalidOperationException( "Attempt to add a row before defining a recordset" );

            _recordsToRetrieve.Last().Add( columnValues );

            return this;
        }

        /// <summary>
        /// Set the datareader in playback mode.  No more recordsets or rows may be added,
        /// but the datareader is ready to pull data from.
        /// </summary>
        /// <returns>The current instance, for chaining calls.</returns>
        public DataReaderMock Playback()
        {
            _readyForPlayback = true;

            return this;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            ThrowUnlessInPlaybackMode();

            _recordsetNumber = 0;
            _rowNumber = -1;
            _isClosed = false;
        }

        private void ThrowIfInPlaybackMode()
        {
            if ( _readyForPlayback )
                throw new InvalidOperationException( "Cannot add records while in Playback state." );
        }

        private void ThrowUnlessInPlaybackMode()
        {
            if ( !_readyForPlayback )
                throw new InvalidOperationException( "Cannot execute outside of Playback state." );
        }

        private object GetColumnValue( int columnOrdinal )
        {
            ThrowUnlessInPlaybackMode();

            return _recordsToRetrieve[ _recordsetNumber ][ _rowNumber ][ columnOrdinal ];
        }

        private int CalculateColumnSize(int columnOrdinal)
        {
            ThrowUnlessInPlaybackMode();

            object column = _recordsToRetrieve[ _recordsetNumber ][ 0 ][ columnOrdinal ];

            if ( column is string )
                return
                    _recordsToRetrieve[ _recordsetNumber ].Select( x => ( (string)x[ columnOrdinal ] ).Length ).Max();

            return Marshal.SizeOf( column );
        }
    }

    /// <summary>
    /// Utility class to simplify test code.
    /// </summary>
    public static partial class A
    {
        /// <summary>
        /// Gets a mock data reader.
        /// </summary>
        /// <value>The data reader.</value>
        public static IDataReader DataReader { get { return new DataReaderMock(); } }
    }
}