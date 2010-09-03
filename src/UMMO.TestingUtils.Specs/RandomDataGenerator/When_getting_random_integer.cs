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
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    [Subject(typeof(RandomInteger))]
    public class When_getting_random_integer : RandomDataGeneratorTestBase
    {
        private const int ExpectedInteger = 22;
        private Establish Context = () => Random.Stub( x => x.Next() ).Return( ExpectedInteger );
        private Because Of = () => _randomInteger = RandomDataGeneratorUnderTest.Integer;

        private It Should_be_of_type_random_integer
            = () => _randomInteger.ShouldBeOfType< RandomInteger >();

        private It Should_return_expected_integer_when_calling_value
            = () => ( (RandomInteger)_randomInteger ).Value.ShouldEqual( ExpectedInteger );

        private It Should_implicitly_cast_to_integer
            = () =>
                  {
                      int value = (RandomInteger)_randomInteger;
                      value.ShouldEqual( ExpectedInteger );
                  };

        private static Object _randomInteger;
    }

    [Subject(typeof(RandomInteger))]
    public class When_getting_random_integer_between_two_values
    {
        private Establish Context = () =>
        {
            _randomInteger = A.Random.Integer;
            _maxValue = A.Random.Integer;
            _minValue = A.Random.Integer.LessThan(_maxValue);
        };

        private Because Of = () => _randomValue = _randomInteger.Between(_minValue, _maxValue);

        private It Should_be_greater_than_or_equal_to_minvalue
            = () => _randomValue.ShouldBeGreaterThanOrEqualTo(_minValue);

        private It Should_be_less_than_or_equal_to_maxvalue
            = () => _randomValue.ShouldBeLessThanOrEqualTo(_maxValue);

        private static RandomInteger _randomInteger;
        private static int _minValue;
        private static int _maxValue;
        private static int _randomValue;
    }
}