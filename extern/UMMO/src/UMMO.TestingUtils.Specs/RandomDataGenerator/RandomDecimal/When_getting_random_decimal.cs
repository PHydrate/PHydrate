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

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomDecimal
{
    [Subject(typeof(RandomData.RandomDecimal))]
    public class When_getting_random_decimal : RandomDataGeneratorTestBase
    {
        private const decimal ExpectedDecimal = 22.5m;
        private Establish Context = () => Random.Stub( x => x.NextDecimal() ).Return( ExpectedDecimal );
        private Because Of = () => _randomDecimal = RandomDataGeneratorUnderTest.Decimal;

        private It Should_be_of_type_random_decimal
            = () => _randomDecimal.ShouldBeOfType< RandomData.RandomDecimal >();

        private It Should_return_expected_decimal_when_calling_value
            = () => ( (RandomData.RandomDecimal)_randomDecimal ).Value.ShouldEqual( ExpectedDecimal );

        private It Should_implicitly_cast_to_decimal
            = () =>
                  {
                      decimal value = (RandomData.RandomDecimal)_randomDecimal;
                      value.ShouldEqual( ExpectedDecimal );
                  };

        private static Object _randomDecimal;
    }
}