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
using Rhino.Mocks;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomLong
{
    [ Subject( typeof(RandomData.RandomLong) ) ]
    public class When_getting_random_long : RandomDataGeneratorTestBase
    {
        private static long _expectedLong;
        private static byte[] _expectedBytes;

        private Establish Context = () =>
                                        {
                                            _expectedBytes = new byte[sizeof(long)];
                                            new Random().NextBytes( _expectedBytes );
                                            _expectedBytes[ sizeof(long) - 1 ] &= 0x7f;
                                            _expectedLong = BitConverter.ToInt64( _expectedBytes, 0 );
                                            Random.Stub( x => x.NextLong() ).Return( _expectedLong );
                                        };
        private Because Of = () => _randomLong = RandomDataGeneratorUnderTest.LongInteger;

        private It Should_be_of_type_random_long
            = () => _randomLong.ShouldBeOfType<RandomData.RandomLong>();

        private It Should_return_expected_long_when_calling_value
            = () => ((RandomData.RandomLong)_randomLong).Value.ShouldEqual(_expectedLong);

        private It Should_implicitly_cast_to_long
            = () =>
            {
                long value = (RandomData.RandomLong)_randomLong;
                value.ShouldEqual(_expectedLong);
            };

        private static Object _randomLong;
    }
}