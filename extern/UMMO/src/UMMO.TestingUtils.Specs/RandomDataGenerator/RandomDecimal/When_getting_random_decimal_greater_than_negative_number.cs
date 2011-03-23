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

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomDecimal
{
    [Subject(typeof(RandomData.RandomDecimal))]
    public class When_getting_random_decimal_greater_than_negative_number
    {
        private Establish Context = () => _minValue = A.Random.Decimal.LessThan( 0m );

        private Because Of = () => _actualValue = A.Random.Decimal.GreaterThan( _minValue );

        private It Should_return_value_greater_than_or_equal_to_min_value
            = () => _actualValue.ShouldBeGreaterThanOrEqualTo( _minValue );

        private static decimal _minValue;
        private static decimal _actualValue;
    }
}