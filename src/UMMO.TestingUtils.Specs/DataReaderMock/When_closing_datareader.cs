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

using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock) )]
    public class When_closing_datareader : DataReaderMockSpecsBase
    {
        Because of = () =>
                         {
                             MockUnderTest.Playback();
                             MockUnderTest.Close();
                         };

        It should_be_closed
            = () => MockUnderTest.IsClosed.ShouldBeTrue();

        It should_return_negative_one_from_recordsaffected
            = () => MockUnderTest.RecordsAffected.ShouldEqual( -1 );
    }
}
