/*
 * This file is part of UMMO.
 *
 *  UMMO is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  UMMO is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with UMMO.  If not, see <http://www.gnu.org/licenses/>.
 *  
 * Copyright 2010, Stephen Michael Czetty
 */

using System.Data;
using System.Text;
using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock) )]
    public class When_calling_get_functions : DataReaderMockSpecsWithRecordSetDefined
    {
        static string _expectedValue = A.Random.Password;
        Because of = () => SetupTestRecord( _expectedValue );

        It should_return_only_the_number_of_columns_into_the_array_passed_to_getvalues
            = () =>
                  {
                      var array = new object[2];
                      MockUnderTest.GetValues( array );
                      array[0].ShouldEqual( _expectedValue );
                      array[1].ShouldBeNull();
                  };

        It should_return_zero_if_getbytes_is_called_with_a_negative_field_offset
            = () => MockUnderTest.GetBytes( 0, -1, new byte[0], 0, 0 ).ShouldEqual( 0 );

        It should_return_zero_if_getchars_is_called_with_a_negative_field_offset
            = () => MockUnderTest.GetChars( 0, -1, new char[0], 0, 0 ).ShouldEqual( 0 );

        It should_fill_buffer_with_bytes_from_string_when_getbytes_is_called
            = AssertThatArrayFromGetBytesIsCorrect;

        It should_fill_buffer_with_chars_from_string_when_getchars_is_called
            = AssertThatArrayFromGetCharsIsCorrect;

        It should_create_datatable_with_correct_data_when_getschematable_is_called
            = () => AssertThatDataTableFromGetSchemaTableIsCorrect( MockUnderTest.GetSchemaTable() );

        static void AssertThatDataTableFromGetSchemaTableIsCorrect( DataTable schemaTable )
        {
            schemaTable.Columns.Count.ShouldEqual( 3 );
            schemaTable.Rows.Count.ShouldEqual( 1 );  
            schemaTable.Rows[0]["ColumnName"].ShouldEqual( ColumnName );
            schemaTable.Rows[0]["ColumnOrdinal"].ShouldEqual( 0 );
            schemaTable.Rows[0]["DataType"].ShouldEqual( _expectedValue.GetType() );
        }

        static void AssertThatArrayFromGetCharsIsCorrect()
        {
            char[] expectedArray = _expectedValue.ToCharArray();
            var buffer = new char[expectedArray.Length];
            MockUnderTest.GetChars( 0, 0, buffer, 0, expectedArray.Length ).ShouldEqual( expectedArray.Length );
            buffer.ShouldEqual( expectedArray );  
        }

        static void AssertThatArrayFromGetBytesIsCorrect()
        {
            byte[] expectedArray = Encoding.Default.GetBytes( _expectedValue );
            var buffer = new byte[expectedArray.Length];
            MockUnderTest.GetBytes( 0, 0, buffer, 0, expectedArray.Length ).ShouldEqual( expectedArray.Length );
            buffer.ShouldEqual( expectedArray );  
            
        }
    }
}
