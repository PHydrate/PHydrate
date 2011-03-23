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

using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomLong
{
    [Subject(typeof(RandomData.RandomLong))]
    public class When_getting_random_long_between_two_values
    {
        private Establish Context = () =>
                                        {
                                            _randomLong = A.Random.LongInteger;
                                            _maxValue = A.Random.LongInteger;
                                            _minValue = A.Random.LongInteger.LessThan(_maxValue);
                                        };

        private Because Of = () => _randomValue = _randomLong.Between(_minValue, _maxValue);

        private It Should_be_greater_than_or_equal_to_minvalue
            = () => _randomValue.ShouldBeGreaterThanOrEqualTo(_minValue);

        // TODO: This test fails intermittently.  There must be an error somewhere.
        private It Should_be_less_than_or_equal_to_maxvalue
            = () => _randomValue.ShouldBeLessThanOrEqualTo(_maxValue);

        private static RandomData.RandomLong _randomLong;
        private static long _minValue;
        private static long _maxValue;
        private static long _randomValue;
    }
}