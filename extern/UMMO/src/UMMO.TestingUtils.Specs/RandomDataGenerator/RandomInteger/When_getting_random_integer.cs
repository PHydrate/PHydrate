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

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomInteger
{
    [Subject(typeof(RandomData.RandomInteger))]
    public class When_getting_random_integer : RandomDataGeneratorTestBase
    {
        private const int ExpectedInteger = 22;
        private Establish Context = () => Random.Stub( x => x.Next() ).Return( ExpectedInteger );
        private Because Of = () => _randomInteger = RandomDataGeneratorUnderTest.Integer;

        private It Should_be_of_type_random_integer
            = () => _randomInteger.ShouldBeOfType< RandomData.RandomInteger >();

        private It Should_return_expected_integer_when_calling_value
            = () => ( (RandomData.RandomInteger)_randomInteger ).Value.ShouldEqual( ExpectedInteger );

        private It Should_implicitly_cast_to_integer
            = () =>
                  {
                      int value = (RandomData.RandomInteger)_randomInteger;
                      value.ShouldEqual( ExpectedInteger );
                  };

        private static Object _randomInteger;
    }
}