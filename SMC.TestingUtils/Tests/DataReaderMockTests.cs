using System;
using System.Data;
using Machine.Specifications;

namespace SMC.TestingUtils.Tests
{
    [Subject(typeof(DataReaderMock))]
    public class When_not_in_playback_mode : DataReaderMockTests
    {
        It should_throw_exception_when_calling_dispose
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.Close() );

        It should_throw_exception_when_calling_getname
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetName( 0 ) );

        It should_throw_exception_when_calling_getdatatypename
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetDataTypeName( 0 ) );

        It should_throw_exception_when_calling_getfieldtype
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetFieldType( 0 ) );

        It should_throw_exception_when_calling_getvalue
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetValue( 0 ) );

        It should_throw_exception_when_calling_getvalues
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetValues( new object[] { } ) );

        It should_throw_exception_when_calling_getordinal
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetOrdinal( "" ) );

        It should_throw_exception_when_calling_getboolean
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetBoolean( 0 ) );

        It should_throw_exception_when_calling_getbyte
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetByte( 0 ) );

        It should_throw_exception_when_calling_getbytes
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetBytes( 0, 0, new byte[] { }, 0, 0 ) );

        It should_throw_exception_when_calling_getchar
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetChar( 0 ) );

        It should_throw_exception_when_calling_getchars
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetChars( 0, 0, new char[] { }, 0, 0 ) );

        It should_throw_exception_when_calling_getguid
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetGuid( 0 ) );

        It should_throw_exception_when_calling_getint16
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetInt16( 0 ) );

        It should_throw_exception_when_calling_getint32
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetInt32( 0 ) );

        It should_throw_exception_when_calling_getint64
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetInt64( 0 ) );

        It should_throw_exception_when_calling_getfloat
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetFloat( 0 ) );

        It should_throw_exception_when_calling_getdouble
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetDouble( 0 ) );

        It should_throw_exception_when_calling_getstring
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetString( 0 ) );

        It should_throw_exception_when_calling_getdecimal
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetDecimal( 0 ) );

        It should_throw_exception_when_calling_getdatetime
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetDateTime( 0 ) );

        It should_throw_exception_when_calling_getdata
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetData( 0 ) );

        It should_throw_exception_when_calling_isdbnull
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.IsDBNull( 0 ) );

        It should_throw_exception_when_referencing_fieldcount
            = () => ShouldThrowInvalidOperationException(() => { if ( MockUnderTest.FieldCount > 0 ) return; } );

        It should_throw_exception_when_referencing_this_by_int
            = () => ShouldThrowInvalidOperationException( () => { if ( ((IDataRecord)MockUnderTest)[0] == null ) return; } );

        It should_throw_exception_when_referencing_this_by_string
            = () => ShouldThrowInvalidOperationException(() => { if (((IDataRecord)MockUnderTest)[""] == null) return; });

        It should_throw_exception_when_calling_close
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.Close() );

        It should_throw_exception_when_calling_getschematable
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetSchemaTable() );

        It should_throw_exception_when_calling_nextresult
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.NextResult() );

        It should_throw_exception_when_calling_read
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.Read() );

        It should_throw_exception_when_referencing_depth
            = () => ShouldThrowInvalidOperationException( () => { if ( MockUnderTest.Depth == 0 ) return; } );

        It should_throw_exception_when_referencing_isclosed
            = () => ShouldThrowInvalidOperationException( () => { if ( MockUnderTest.IsClosed ) return; } );

        It should_throw_exception_when_referencing_recordsaffected
            = () => ShouldThrowInvalidOperationException( () => { if ( MockUnderTest.RecordsAffected == 0 ) return; } ); 

        static Exception ShouldThrowInvalidOperationException(Action action)
        {
            return (typeof(InvalidOperationException)).ShouldBeThrownBy( action );
        }
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_bool_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow(BooleanValue).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName(0).ShouldEqual(ColumnName);

        It should_return_boolean_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName(0).ShouldEqual("Boolean");

        It should_return_typeof_boolean_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType(0).ShouldEqual(typeof(Boolean));

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue(0).ShouldEqual(BooleanValue);

        // TODO: Test GetValues

        It should_return_value_when_getboolean_is_called
            = () => MockUnderTest.GetBoolean( 0 ).ShouldEqual( BooleanValue );  

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetByte(0));

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetChar(0));

        // TODO: Test GetChars

        It should_throw_exception_when_getguid_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetGuid(0));

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt16(0));

        It should_return_value_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetInt32( 0 ) );

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt64(0));

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetFloat(0));

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDouble(0));

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetString(0));

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDecimal(0));

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDateTime(0));

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual(BooleanValue);

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual(BooleanValue);

        static readonly bool BooleanValue = A.Random.Boolean;
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_byte_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow(ByteValue).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName(0).ShouldEqual(ColumnName);

        It should_return_byte_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName(0).ShouldEqual("Byte");

        It should_return_typeof_byte_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType(0).ShouldEqual(typeof(Byte));

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue(0).ShouldEqual(ByteValue);

        // TODO: Test GetValues

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetBoolean(0));

        It should_throw_exception_when_getbyte_is_called
            = () => MockUnderTest.GetByte( 0 ).ShouldEqual( ByteValue );  

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetChar(0));

        // TODO: Test GetChars

        It should_throw_exception_when_getguid_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetGuid(0));

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt16(0));

        It should_return_value_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt32(0));

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt64(0));

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetFloat(0));

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDouble(0));

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetString(0));

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDecimal(0));

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDateTime(0));

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual(ByteValue);

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual(ByteValue);

        static readonly byte ByteValue = A.Random.Byte;
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_char_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow(CharValue).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName(0).ShouldEqual(ColumnName);

        It should_return_int32_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName(0).ShouldEqual("Char");

        It should_return_typeof_int32_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType(0).ShouldEqual(typeof(Char));

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue(0).ShouldEqual(CharValue);

        // TODO: Test GetValues

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetBoolean(0));

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetByte(0));

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => MockUnderTest.GetChar( 0 ).ShouldEqual( CharValue );  

        // TODO: Test GetChars

        It should_throw_exception_when_getguid_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetGuid(0));

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt16(0));

        It should_return_value_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetInt32( 0 ) );

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt64(0));

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetFloat(0));

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDouble(0));

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetString(0));

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDecimal(0));

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDateTime(0));

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual(CharValue);

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual(CharValue);

        static readonly char CharValue = A.Random.Character;
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_guid_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow(GuidValue).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName(0).ShouldEqual(ColumnName);

        It should_return_guid_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName(0).ShouldEqual("Guid");

        It should_return_typeof_guid_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType(0).ShouldEqual(typeof(Guid));

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue(0).ShouldEqual(GuidValue);

        // TODO: Test GetValues

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetBoolean(0));

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetByte(0));

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetChar( 0 ) );

        // TODO: Test GetChars

        It should_return_value_when_getguid_is_called
            = () => MockUnderTest.GetGuid( 0 ).ShouldEqual( GuidValue );  

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt16(0));

        It should_return_value_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt32(0));

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt64(0));

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetFloat(0));

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDouble(0));

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetString(0));

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDecimal(0));

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDateTime(0));

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual(GuidValue);

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual(GuidValue);

        static readonly Guid GuidValue = RandomDataGenerator.Guid;
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_short_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow(ShortValue).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName(0).ShouldEqual(ColumnName);

        It should_return_int16_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName(0).ShouldEqual("Int16");

        It should_return_typeof_int16_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType(0).ShouldEqual(typeof(Int16));

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue(0).ShouldEqual(ShortValue);

        // TODO: Test GetValues

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetBoolean(0));

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetByte(0));

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetChar( 0 ) );

        // TODO: Test GetChars

        It should_throw_exception_when_getguid_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetGuid(0));

        It should_return_value_when_getint16_is_called
            = () => MockUnderTest.GetInt16( 0 ).ShouldEqual( ShortValue );  

        It should_throw_exception_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt32(0));

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt64(0));

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetFloat(0));

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDouble(0));

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetString(0));

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDecimal(0));

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDateTime(0));

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual(ShortValue);

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual(ShortValue);

        static readonly short ShortValue = A.Random.Short;
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_int_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow( IntegerValue ).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName( 0 ).ShouldEqual( ColumnName );

        It should_return_int32_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName( 0 ).ShouldEqual( "Int32" );

        It should_return_typeof_int32_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType( 0 ).ShouldEqual( typeof(Int32) );

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue( 0 ).ShouldEqual( IntegerValue );
    
        // TODO: Test GetValues

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetBoolean( 0 ) );

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetByte( 0 ) );

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetChar( 0 ) );

        // TODO: Test GetChars

        It should_throw_exception_when_getguid_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetGuid( 0 ) );

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetInt16( 0 ) );

        It should_return_value_when_getint32_is_called
            = () => MockUnderTest.GetInt32( 0 ).ShouldEqual( IntegerValue );

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetInt64( 0 ) );

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetFloat( 0 ) );

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetDouble( 0 ) );

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetString( 0 ) );

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetDecimal( 0 ) );

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetDateTime( 0 ) );

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull( 0 ).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual( IntegerValue );

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual( IntegerValue );

        static readonly int IntegerValue = A.Random.Integer;
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_long_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow(LongValue).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName(0).ShouldEqual(ColumnName);

        It should_return_int64_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName(0).ShouldEqual("Int64");

        It should_return_typeof_int64_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType(0).ShouldEqual(typeof(Int64));

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue(0).ShouldEqual(LongValue);

        // TODO: Test GetValues

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetBoolean(0));

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetByte(0));

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetChar( 0 ) );

        // TODO: Test GetChars

        It should_throw_exception_when_getguid_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetGuid(0));

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt16(0));

        It should_throw_exception_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt32(0));

        It should_return_value_when_getint64_is_called
            = () => MockUnderTest.GetInt64( 0 ).ShouldEqual( LongValue );  

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetFloat(0));

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDouble(0));

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetString(0));

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDecimal(0));

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDateTime(0));

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual(LongValue);

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual(LongValue);

        static readonly long LongValue = A.Random.LongInteger;
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_float_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow(FloatValue).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName(0).ShouldEqual(ColumnName);

        It should_return_float_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName(0).ShouldEqual("Single");

        It should_return_typeof_int32_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType(0).ShouldEqual(typeof(Single));

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue(0).ShouldEqual(FloatValue);

        // TODO: Test GetValues

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetBoolean(0));

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetByte(0));

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetChar( 0 ) );

        // TODO: Test GetChars

        It should_throw_exception_when_getguid_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetGuid(0));

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt16(0));

        It should_return_value_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt32(0));

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt64(0));

        It should_throw_exception_when_getfloat_is_called
            = () => MockUnderTest.GetFloat( 0 ).ShouldEqual( FloatValue ); 

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDouble(0));

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetString(0));

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDecimal(0));

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDateTime(0));

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual(FloatValue);

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual(FloatValue);

        static readonly float FloatValue = A.Random.Float;
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_double_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow(DoubleValue).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName(0).ShouldEqual(ColumnName);

        It should_return_double_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName(0).ShouldEqual("Double");

        It should_return_typeof_double_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType(0).ShouldEqual(typeof(Double));

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue(0).ShouldEqual(DoubleValue);

        // TODO: Test GetValues

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetBoolean(0));

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetByte(0));

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetChar( 0 ) );

        // TODO: Test GetChars

        It should_throw_exception_when_getguid_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetGuid(0));

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt16(0));

        It should_return_value_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt32(0));

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt64(0));

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetFloat(0));

        It should_return_value_when_getdouble_is_called
            = () => MockUnderTest.GetDouble( 0 ).ShouldEqual( DoubleValue );  

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetString(0));

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDecimal(0));

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDateTime(0));

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual(DoubleValue);

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual(DoubleValue);

        static readonly double DoubleValue = A.Random.Double;
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_string_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow(StringValue).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName(0).ShouldEqual(ColumnName);

        It should_return_string_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName(0).ShouldEqual("String");

        It should_return_typeof_string_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType(0).ShouldEqual(typeof(String));

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue(0).ShouldEqual(StringValue);

        // TODO: Test GetValues

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetBoolean(0));

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetByte(0));

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetChar( 0 ) );

        // TODO: Test GetChars

        It should_throw_exception_when_getguid_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetGuid(0));

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt16(0));

        It should_return_value_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt32(0));

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt64(0));

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetFloat(0));

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDouble(0));

        It should_return_value_when_getstring_is_called
            = () => MockUnderTest.GetString( 0 ).ShouldEqual( StringValue );  

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDecimal(0));

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDateTime(0));

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual(StringValue);

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual(StringValue);

        static readonly string StringValue = A.Random.Noun;
    }

    [Subject(typeof(DataReaderMock))]
    public class When_column_is_of_decimal_type : DataReaderMockTests
    {
        Because of = () => MockUnderTest.AddRow(DecimalValue).Playback();

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName(0).ShouldEqual(ColumnName);

        It should_return_decimal_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName(0).ShouldEqual("Decimal");

        It should_return_typeof_int32_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType(0).ShouldEqual(typeof(Decimal));

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue(0).ShouldEqual(DecimalValue);

        // TODO: Test GetValues

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetBoolean(0));

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetByte(0));

        // TODO: Test GetBytes

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetChar( 0 ) );

        // TODO: Test GetChars

        It should_throw_exception_when_getguid_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetGuid(0));

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt16(0));

        It should_return_value_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt32(0));

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetInt64(0));

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetFloat(0));

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDouble(0));

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetString(0));

        It should_return_value_when_getdecimal_is_called
            = () => MockUnderTest.GetDecimal( 0 ).ShouldEqual( DecimalValue );  

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy(() => MockUnderTest.GetDateTime(0));

        // TODO: Test GetData()

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual(DecimalValue);

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual(DecimalValue);

        static readonly decimal DecimalValue = A.Random.Decimal;
    }


    [Subject(typeof(DataReaderMock))]
    public class When_querying_datareader_metadata : DataReaderMockTests
    {
        Because of = () => MockUnderTest.Playback();

        It should_return_one_when_fieldcount_is_called
            = () => MockUnderTest.FieldCount.ShouldEqual( 1 );

        It should_return_zero_when_getordinal_is_called_with_columnname
            = () => MockUnderTest.GetOrdinal( ColumnName ).ShouldEqual( 0 );

        It should_throw_exception_when_getordinal_is_called_with_unknown_columnname
            = () =>
              typeof(IndexOutOfRangeException).ShouldBeThrownBy( () => MockUnderTest.GetOrdinal( A.Random.LastName ) );
    }

    public class DataReaderMockTests
    {
        Establish context = () =>
                                {
                                    MockUnderTest = new DataReaderMock();
                                    ColumnName = A.Random.Noun;
                                    MockUnderTest.AddRecordSet( ColumnName );  
                                };

        protected static DataReaderMock MockUnderTest;
        protected static string ColumnName;
    }
}