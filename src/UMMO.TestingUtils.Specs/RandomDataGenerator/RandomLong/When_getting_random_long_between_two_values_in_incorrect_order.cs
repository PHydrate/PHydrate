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

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomLong
{
    [Subject(typeof(RandomData.RandomLong))]
    public class When_getting_random_long_between_two_values_in_incorrect_order
    {
        private Establish Context = () =>
                                        {
                                            _randomLong = A.Random.LongInteger;
                                            _minValue = A.Random.LongInteger;
                                            _maxValue = A.Random.LongInteger.LessThan(_minValue);
                                        };

        private Because Of = () => _exception = Catch.Exception(() => _randomLong.Between(_minValue, _maxValue));

        private It Should_throw_exception_when_minvalue_is_greater_than_maxvalue
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_exception_of_type_argument_exception
            = () => _exception.ShouldBeOfType<ArgumentException>();

        private static RandomData.RandomLong _randomLong;
        private static long _minValue;
        private static long _maxValue;
        private static Exception _exception;
    }
}