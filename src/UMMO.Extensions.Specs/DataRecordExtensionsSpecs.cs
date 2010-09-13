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
using UMMO.TestingUtils;

namespace UMMO.Extensions.Specs
{
    [ Subject( typeof(DataRecordExtensions) ) ]
    public class When_calling_data_record_value_extension_method
    {
        private static IDataReader _dataRecordUnderTest;
        private static DateTime _randomDate;
        private static string _randomString;
        private static int _randomInt;

        private Establish Context = () =>
                                        {
                                            var record = new DataReaderMock();
                                            record.AddRecordSet( "string", "int", "date", "enum", "nullString",
                                                                 "nullInt", "nullDate", "nullEnum", "enumAsInt" );
                                            _randomDate = A.Random.DateTime;
                                            _randomString = A.Random.String.Resembling.A.FirstName;
                                            _randomInt = A.Random.Integer;
                                            record.AddRow( _randomString, _randomInt, _randomDate, "X", null, null, null,
                                                           null, 1 );
                                            record.Playback();
                                            _dataRecordUnderTest = record;
                                        };

        private Because Of = () => _dataRecordUnderTest.Read();

        private It Should_be_the_expected_date_value
            = () => _dataRecordUnderTest.Value< DateTime >( "date" ).ShouldEqual( _randomDate );

        private It Should_be_the_expected_enum_value
            = () => _dataRecordUnderTest.Value< TestEnum >( "enum" ).ShouldEqual( TestEnum.X );

        private It Should_be_the_expected_int_value
            = () => _dataRecordUnderTest.Value< int >( "int" ).ShouldEqual( _randomInt );

        private It Should_be_the_expected_string_value
            = () => _dataRecordUnderTest.Value< string >( "string" ).ShouldEqual( _randomString );

        private It Should_return_a_string_for_the_string_column
            = () => _dataRecordUnderTest.Value< string >( "string" ).ShouldBeOfType< string >();

        private It Should_return_an_date_for_the_date_column
            = () => _dataRecordUnderTest.Value< DateTime >( "date" ).ShouldBeOfType< DateTime >();

        private It Should_return_an_enum_for_the_enum_column
            = () => _dataRecordUnderTest.Value< TestEnum >( "enum" ).ShouldBeOfType< TestEnum >();

        private It Should_return_an_int_for_the_int_column
            = () => _dataRecordUnderTest.Value< int >( "int" ).ShouldBeOfType< int >();

        private It Should_return_null_for_the_nullstring_column
            = () => _dataRecordUnderTest.Value< string >( "nullString" ).ShouldBeNull();

        private It Should_return_testenum_none_for_the_nullenum_column
            = () => _dataRecordUnderTest.Value< TestEnum >( "nullEnum" ).ShouldEqual( TestEnum.None );

        private It Should_return_the_default_date_for_the_nulldate_column
            = () => _dataRecordUnderTest.Value< DateTime >( "nullDate" ).ShouldEqual( default( DateTime ) );

        private It Should_return_zero_for_the_nullint_column
            = () => _dataRecordUnderTest.Value< int >( "nullInt" ).ShouldEqual( 0 );

        private It Should_return_a_value_when_called_with_an_ordinal
            = () => _dataRecordUnderTest.Value< string >( 0 ).ShouldEqual( _randomString );

        private It Should_return_enum_when_value_is_an_int
            = () => _dataRecordUnderTest.Value< TestEnum >( "enumAsInt" ).ShouldEqual( TestEnum.X );

        #region Nested type: TestEnum

        private enum TestEnum
        {
            None = 0,
            X = 1
        }

        #endregion
    }
}