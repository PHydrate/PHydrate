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
    public class When_querying_datareader_metadata : DataReaderMockSpecsWithRecordSetDefined
    {
        private Because Of = () => MockUnderTest.Playback();

        private It Should_return_one_when_fieldcount_is_called
            = () => MockUnderTest.FieldCount.ShouldEqual( 1 );

        private It Should_return_zero_when_getordinal_is_called_with_columnname
            = () => MockUnderTest.GetOrdinal( ColumnName ).ShouldEqual( 0 );

        private It Should_throw_exception_when_getordinal_is_called_with_unknown_columnname
            = () =>
              typeof(IndexOutOfRangeException).ShouldBeThrownBy( () => MockUnderTest.GetOrdinal( A.Random.LastName ) );
    }
}