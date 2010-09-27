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

using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [ Subject( typeof(TestingUtils.DataReaderMock) ) ]
    public class When_calling_get_functions : DataReaderMockSpecsWithRecordSetDefined<string>
    {
        private Establish Context =()=> ExpectedValue = A.Random.String.Resembling.A.Password;
        

        private It Should_create_datatable_with_correct_data_when_getschematable_is_called
            = () => AssertThatDataTableFromGetSchemaTableIsCorrect( MockUnderTest.GetSchemaTable() );

        private It Should_fill_buffer_with_bytes_from_string_when_getbytes_is_called
            = AssertThatArrayFromGetBytesIsCorrect;

        private It Should_fill_buffer_with_chars_from_string_when_getchars_is_called
            = AssertThatArrayFromGetCharsIsCorrect;

        private It Should_return_only_the_number_of_columns_into_the_array_passed_to_getvalues
            = () =>
                  {
                      var array = new object[2];
                      MockUnderTest.GetValues( array );
                      array[ 0 ].ShouldEqual( ExpectedValue );
                      array[ 1 ].ShouldBeNull();
                  };

        private It Should_return_zero_if_getbytes_is_called_with_a_negative_field_offset
            = () => MockUnderTest.GetBytes( 0, -1, new byte[0], 0, 0 ).ShouldEqual( 0 );

        private It Should_return_zero_if_getchars_is_called_with_a_negative_field_offset
            = () => MockUnderTest.GetChars( 0, -1, new char[0], 0, 0 ).ShouldEqual( 0 );

        private static void AssertThatDataTableFromGetSchemaTableIsCorrect( DataTable schemaTable )
        {
            schemaTable.Columns.Count.ShouldEqual( 4 );
            schemaTable.Rows.Count.ShouldEqual( 1 );
            schemaTable.Rows[ 0 ][ "ColumnName" ].ShouldEqual( ColumnName );
            schemaTable.Rows[ 0 ][ "ColumnOrdinal" ].ShouldEqual( 0 );
            schemaTable.Rows[ 0 ][ "ColumnSize" ].ShouldEqual( ExpectedValue.Length );
            schemaTable.Rows[ 0 ][ "DataType" ].ShouldEqual( ExpectedValue.GetType() );
        }

        private static void AssertThatArrayFromGetCharsIsCorrect()
        {
            char[] expectedArray = ExpectedValue.ToCharArray();
            var buffer = new char[expectedArray.Length];
            MockUnderTest.GetChars( 0, 0, buffer, 0, expectedArray.Length ).ShouldEqual( expectedArray.Length );
            buffer.ShouldEqual( expectedArray );
        }

        private static void AssertThatArrayFromGetBytesIsCorrect()
        {
            byte[] expectedArray = Encoding.Default.GetBytes( ExpectedValue );
            var buffer = new byte[expectedArray.Length];
            MockUnderTest.GetBytes( 0, 0, buffer, 0, expectedArray.Length ).ShouldEqual( expectedArray.Length );
            buffer.ShouldEqual( expectedArray );
        }
    }

    [Subject(typeof(TestingUtils.DataReaderMock))]
    public class When_calling_get_functions_with_numeric_datatype : DataReaderMockSpecsWithRecordSetDefined<int>
    {
        private Establish Context =()=> ExpectedValue = A.Random.Integer;

        private It Should_create_datatable_with_correct_data_when_getschematable_is_called
            = () => AssertThatDataTableFromGetSchemaTableIsCorrect( MockUnderTest.GetSchemaTable() );

        private static void AssertThatDataTableFromGetSchemaTableIsCorrect(DataTable schemaTable)
        {
            schemaTable.Columns.Count.ShouldEqual(4);
            schemaTable.Rows.Count.ShouldEqual(1);
            schemaTable.Rows[0]["ColumnName"].ShouldEqual(ColumnName);
            schemaTable.Rows[0]["ColumnOrdinal"].ShouldEqual(0);
            schemaTable.Rows[0]["ColumnSize"].ShouldEqual(sizeof(int));
            schemaTable.Rows[0]["DataType"].ShouldEqual(ExpectedValue.GetType());
        }
    }
}