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
using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [Behaviors]
    public class DataReaderBehavior
    {
        protected static TestingUtils.DataReaderMock MockUnderTest;
        protected static string ColumnName;

        private It Should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName( 0 ).ShouldEqual( ColumnName );

        private It Should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();
    }

    [Behaviors]
    public class DataReaderBehavior<T>
    {
        protected static TestingUtils.DataReaderMock MockUnderTest;
        protected static string ColumnName;
        protected static T ExpectedValue;

        private It Should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName( 0 ).ShouldEqual( ColumnName );

        private It Should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull(0).ShouldBeFalse();

        private It Should_return_correct_type_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName( 0 ).ShouldEqual( typeof(T).Name );

        private It Should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue( 0 ).ShouldEqual( ExpectedValue );

        private It Should_return_type_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType( 0 ).ShouldEqual( typeof(T) );

        private It Should_return_valid_datareader_when_getdata_is_called
            = () => AssertThatDataReaderFromGetDataIsCorrect( MockUnderTest.GetData( 0 ), ExpectedValue );

        private It Should_return_valid_value_in_array_when_getvalues_is_called
            = () => AssertThatArrayFromGetValuesIsCorrect( ExpectedValue );

        private It Should_return_value_when_name_indexer_is_used
            = () => ( (IDataReader)MockUnderTest )[ ColumnName ].ShouldEqual( ExpectedValue );

        private It Should_return_value_when_ordinal_indexer_is_used
            = () => ( (IDataReader)MockUnderTest )[ 0 ].ShouldEqual( ExpectedValue );

        private static void AssertThatDataReaderFromGetDataIsCorrect(IDataReader dataReader, T expectedValue)
        {
            dataReader.Read().ShouldBeTrue();
            dataReader[ 0 ].ShouldBeOfType< T >();
            dataReader[ 0 ].ShouldEqual( expectedValue );
            dataReader[ ColumnName ].ShouldBeOfType< T >();
            dataReader[ ColumnName ].ShouldEqual( expectedValue );
        }

        private static void AssertThatArrayFromGetValuesIsCorrect(T expectedValue)
        {
            var objArray = new object[1];
            MockUnderTest.GetValues( objArray );
            objArray[ 0 ].ShouldNotBeNull();
            objArray[ 0 ].ShouldBeOfType< T >();
            ( (T)objArray[ 0 ] ).ShouldEqual( expectedValue );
        }
    }
}