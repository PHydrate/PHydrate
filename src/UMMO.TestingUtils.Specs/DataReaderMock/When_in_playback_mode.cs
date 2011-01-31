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
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [Subject(typeof(TestingUtils.DataReaderMock))]
    public class When_in_playback_mode : DataReaderMockSpecsBase
    {
        private Establish Context = () => MockUnderTest.Playback();

        private It Should_return_zero_when_calling_depth
            = () => MockUnderTest.Depth.ShouldEqual(0);

        private It Should_return_zero_when_calling_recordsaffected
            = () => MockUnderTest.RecordsAffected.ShouldEqual(0);

        private It Should_throw_exception_when_adding_a_recordset
            = () => typeof(InvalidOperationException).ShouldBeThrownBy(() => MockUnderTest.AddRecordSet("test"));

        private It Should_throw_exception_when_adding_a_row
            = () => typeof(InvalidOperationException).ShouldBeThrownBy(() => MockUnderTest.AddRow(0));
    }

    [Subject(typeof(TestingUtils.DataReaderMock))]
    public class When_calling_reset : DataReaderMockSpecsBase
    {
        private Establish Context = () =>
                                        {
                                            MockUnderTest.AddRecordSet( "test" );
                                            ExpectedInteger = A.Random.Integer;
                                            MockUnderTest.AddRow( ExpectedInteger );
                                            MockUnderTest.Playback();
                                            MockUnderTest.Read();
                                            MockUnderTest.Reset();
                                        };

        private Because Of = () => MockUnderTest.Read();

        private It Should_return_record
            = () => MockUnderTest.GetValue( 0 ).ShouldEqual( ExpectedInteger );

        private static int ExpectedInteger;
    }
}