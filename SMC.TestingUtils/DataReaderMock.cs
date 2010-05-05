using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            throw new NotImplementedException();
        }


        public bool GetBoolean( int i )
        {
            throw new NotImplementedException();
        }


        public byte GetByte( int i )
        {
            throw new NotImplementedException();
        }


        public long GetBytes( int i, long fieldOffset, byte[] buffer, int bufferoffset, int length )
        {
            throw new NotImplementedException();
        }


        public char GetChar( int i )
        {
            throw new NotImplementedException();
        }


        public long GetChars( int i, long fieldoffset, char[] buffer, int bufferoffset, int length )
        {
            throw new NotImplementedException();
        }


        public Guid GetGuid( int i )
        {
            throw new NotImplementedException();
        }


        public short GetInt16( int i )
        {
            throw new NotImplementedException();
        }


        public int GetInt32( int i )
        {
            throw new NotImplementedException();
        }


        public long GetInt64( int i )
        {
            throw new NotImplementedException();
        }


        public float GetFloat( int i )
        {
            throw new NotImplementedException();
        }


        public double GetDouble( int i )
        {
            throw new NotImplementedException();
        }


        public string GetString( int i )
        {
            throw new NotImplementedException();
        }


        public decimal GetDecimal( int i )
        {
            throw new NotImplementedException();
        }


        public DateTime GetDateTime( int i )
        {
            throw new NotImplementedException();
        }


        public IDataReader GetData( int i )
        {
            throw new NotImplementedException();
        }


        public bool IsDBNull( int i )
        {
            throw new NotImplementedException();
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


        public void Close() {}


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
            get { throw new NotImplementedException(); }
        }

        public bool IsClosed
        {
            get { return false; }
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
