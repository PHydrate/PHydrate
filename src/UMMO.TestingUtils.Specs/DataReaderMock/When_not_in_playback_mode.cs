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
using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [ Subject( typeof(TestingUtils.DataReaderMock) ) ]
    public class When_not_in_playback_mode : DataReaderMockSpecsWithRecordSetDefined
    {
        private It Should_throw_exception_when_calling_close
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.Close() );

        private It Should_throw_exception_when_calling_dispose
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.Dispose() );

        private It Should_throw_exception_when_calling_getboolean
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetBoolean( 0 ) );

        private It Should_throw_exception_when_calling_getbyte
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetByte( 0 ) );

        private It Should_throw_exception_when_calling_getbytes
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetBytes( 0, 0, new byte[] { }, 0, 0 ) );

        private It Should_throw_exception_when_calling_getchar
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetChar( 0 ) );

        private It Should_throw_exception_when_calling_getchars
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetChars( 0, 0, new char[] { }, 0, 0 ) );

        private It Should_throw_exception_when_calling_getdata
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetData( 0 ) );

        private It Should_throw_exception_when_calling_getdatatypename
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetDataTypeName( 0 ) );

        private It Should_throw_exception_when_calling_getdatetime
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetDateTime( 0 ) );

        private It Should_throw_exception_when_calling_getdecimal
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetDecimal( 0 ) );

        private It Should_throw_exception_when_calling_getdouble
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetDouble( 0 ) );

        private It Should_throw_exception_when_calling_getfieldtype
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetFieldType( 0 ) );

        private It Should_throw_exception_when_calling_getfloat
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetFloat( 0 ) );

        private It Should_throw_exception_when_calling_getguid
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetGuid( 0 ) );

        private It Should_throw_exception_when_calling_getint16
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetInt16( 0 ) );

        private It Should_throw_exception_when_calling_getint32
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetInt32( 0 ) );

        private It Should_throw_exception_when_calling_getint64
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetInt64( 0 ) );

        private It Should_throw_exception_when_calling_getname
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetName( 0 ) );

        private It Should_throw_exception_when_calling_getordinal
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetOrdinal( "" ) );

        private It Should_throw_exception_when_calling_getschematable
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetSchemaTable() );

        private It Should_throw_exception_when_calling_getstring
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetString( 0 ) );

        private It Should_throw_exception_when_calling_getvalue
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetValue( 0 ) );

        private It Should_throw_exception_when_calling_getvalues
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.GetValues( new object[] { } ) );

        private It Should_throw_exception_when_calling_isdbnull
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.IsDBNull( 0 ) );

        private It Should_throw_exception_when_calling_nextresult
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.NextResult() );

        private It Should_throw_exception_when_calling_read
            = () => ShouldThrowInvalidOperationException( () => MockUnderTest.Read() );

#pragma warning disable 642
        private It Should_throw_exception_when_referencing_depth
            = () => ShouldThrowInvalidOperationException( () =>
                                                              {
                                                                  if ( MockUnderTest.Depth == 0 )
                                                                      ;
                                                              } );

        private It Should_throw_exception_when_referencing_fieldcount
            = () => ShouldThrowInvalidOperationException( () =>
                                                              {
                                                                  if ( MockUnderTest.FieldCount > 0 )
                                                                      ;
                                                              } );

        private It Should_throw_exception_when_referencing_recordsaffected
            = () => ShouldThrowInvalidOperationException( () =>
                                                              {
                                                                  if ( MockUnderTest.RecordsAffected == 0 )
                                                                      ;
                                                              } );

        private It Should_throw_exception_when_referencing_this_by_int
            = () => ShouldThrowInvalidOperationException( () =>
                                                              {
                                                                  if ( ( (IDataRecord)MockUnderTest )[ 0 ] == null )
                                                                      ;
                                                              } );

        private It Should_throw_exception_when_referencing_this_by_string
            = () => ShouldThrowInvalidOperationException( () =>
                                                              {
                                                                  if ( ( (IDataRecord)MockUnderTest )[ "" ] == null )
                                                                      ;
                                                              } );
#pragma warning restore 642

        private static Exception ShouldThrowInvalidOperationException( Action action )
        {
            return ( typeof(InvalidOperationException) ).ShouldBeThrownBy( action );
        }
    }
}