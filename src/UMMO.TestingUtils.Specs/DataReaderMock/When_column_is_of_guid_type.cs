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
using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [ Subject( typeof(TestingUtils.DataReaderMock) ) ]
    public class When_column_is_of_guid_type : DataReaderMockSpecsWithRecordSetDefined<Guid>
    {
        private Establish Context = () => ExpectedValue = A.Random.Guid;

        private Behaves_like<DataReaderBehavior<Guid>> a_datareader_with_guid_column;

       private It Should_return_value_when_getguid_is_called
            = () => MockUnderTest.GetGuid( 0 ).ShouldEqual( ExpectedValue );

        private It Should_throw_exception_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetInt32( 0 ) );

        private It Should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetBoolean( 0 ) );

        private It Should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetByte( 0 ) );

        private It Should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetChar( 0 ) );

        private It Should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetDateTime( 0 ) );

        private It Should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetDecimal( 0 ) );

        private It Should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetDouble( 0 ) );

        private It Should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetFloat( 0 ) );

        private It Should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetInt16( 0 ) );

        private It Should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetInt64( 0 ) );

        private It Should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetString( 0 ) );
    }
}