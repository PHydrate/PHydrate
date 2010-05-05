using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SMC.TestingUtils
{
    public class DataReaderMock : IDataReader
    {
        readonly IList< KeyValuePair< string, object > >[] _recordsToRetrieve;
        int _readCount;
        int _recordsetNumber;
        DataTable _schemaTable;


        public DataReaderMock( params IList< KeyValuePair< string, object > >[] recordsToRetrieve )
        {
            _recordsToRetrieve = recordsToRetrieve;
            _recordsetNumber = 0;
        }

        #region IDataReader Members

        public void Dispose()
        {
            _readCount = 0;
        }


        public string GetName( int i )
        {
            return _recordsToRetrieve[_recordsetNumber][i].Key;
        }


        public string GetDataTypeName( int i )
        {
            return _recordsToRetrieve[_recordsetNumber][i].Value.GetType().Name;
        }


        public Type GetFieldType( int i )
        {
            return _recordsToRetrieve[_recordsetNumber][i].Value.GetType();
        }


        public object GetValue( int i )
        {
            return _recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public int GetValues( object[] values )
        {
            for ( int i = 0; i < values.Length; i++ )
            {
                if ( i >= _recordsToRetrieve[_recordsetNumber].Count )
                    break;

                values[i] = _recordsToRetrieve[_recordsetNumber][i].Value;
            }

            return Math.Min( values.Length, _recordsToRetrieve[_recordsetNumber].Count );
        }


        public int GetOrdinal( string name )
        {
            for (int i = 0; i < _recordsToRetrieve[_recordsetNumber].Count; i++)
                if (_recordsToRetrieve[_recordsetNumber][i].Key == name)
                    return i;

            throw new IndexOutOfRangeException();
        }


        public bool GetBoolean( int i )
        {
            return (bool)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public byte GetByte( int i )
        {
            return (byte)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public long GetBytes( int i, long fieldOffset, byte[] buffer, int bufferoffset, int length )
        {
            if (fieldOffset < 0)
                return 0;

            var str = Encoding.Default.GetBytes( (string)_recordsToRetrieve[_recordsetNumber][i].Value );
            long k = 0;
            for (long j = fieldOffset; j < str.Length && k < length; j++, k++)
                buffer[bufferoffset++] = str[j];

            return k;
        }


        public char GetChar( int i )
        {
            return (char)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public long GetChars( int i, long fieldoffset, char[] buffer, int bufferoffset, int length )
        {
            if (fieldoffset < 0)
                return 0;

            var str = ((string)_recordsToRetrieve[_recordsetNumber][i].Value).ToCharArray();
            long k = 0;
            for ( long j = fieldoffset; j < str.Length && k < length; j++,k++ )
                buffer[bufferoffset++] = str[j];

            return k;
        }


        public Guid GetGuid( int i )
        {
            return (Guid)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public short GetInt16( int i )
        {
            return (short)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public int GetInt32( int i )
        {
            return (int)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public long GetInt64( int i )
        {
            return (long)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public float GetFloat( int i )
        {
            return (float)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public double GetDouble( int i )
        {
            return (double)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public string GetString( int i )
        {
            return (string)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public decimal GetDecimal( int i )
        {
            return (decimal)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public DateTime GetDateTime( int i )
        {
            return (DateTime)_recordsToRetrieve[_recordsetNumber][i].Value;
        }


        public IDataReader GetData( int i )
        {
            return
                new DataReaderMock( new List< KeyValuePair< string, object > >
                                        {
                                            new KeyValuePair< string, object >(
                                                _recordsToRetrieve[_recordsetNumber][i].Key,
                                                _recordsToRetrieve[_recordsetNumber][i].Value )
                                        } );
        }


        public bool IsDBNull( int i )
        {
            return _recordsToRetrieve[_recordsetNumber][i].Value == null;
        }


        public int FieldCount
        {
            get { return _recordsToRetrieve[_recordsetNumber].Count; }
        }


        object IDataRecord.this[ int i ]
        {
            get { return _recordsToRetrieve[_recordsetNumber][i].Value; }
        }


        object IDataRecord.this[ string name ]
        {
            get { return _recordsToRetrieve[_recordsetNumber].Where( x => x.Key == name ).First().Value; }
        }


        public void Close()
        {
            IsClosed = true;
        }


        public DataTable GetSchemaTable()
        {
            if ( _schemaTable == null )
            {
                _schemaTable = new DataTable();
                _schemaTable.Columns.Add( "ColumnName" );
                _schemaTable.Columns.Add( "ColumnOrdinal", typeof(int) );
                _schemaTable.Columns.Add( "DataType", typeof(Type) );

                for ( int i = 0; i < _recordsToRetrieve[_recordsetNumber].Count; i++ )
                {
                    DataRow newRow = _schemaTable.NewRow();
                    newRow["ColumnName"] = _recordsToRetrieve[_recordsetNumber][i].Key;
                    newRow["ColumnOrdinal"] = i;
                    newRow["DataType"] = _recordsToRetrieve[_recordsetNumber][i].Value.GetType();

                    _schemaTable.Rows.Add( newRow );
                }
            }

            return _schemaTable;
        }


        public bool NextResult()
        {
            _readCount = 0;
            return (++_recordsetNumber < _recordsToRetrieve.Length);
        }


        public bool Read()
        {
            return _readCount++ < 1;
        }


        public int Depth
        {
            get { return 0; }
        }

        public bool IsClosed { get; private set; }

        public int RecordsAffected
        {
            get
            {
                if (IsClosed)
                    return -1;
                return 0;
            }
        }

        #endregion
    }
}
