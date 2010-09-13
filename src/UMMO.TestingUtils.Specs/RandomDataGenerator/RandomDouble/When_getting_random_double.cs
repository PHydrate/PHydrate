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

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomDouble
{
    [Subject(typeof(RandomData.RandomDouble))]
    public class When_getting_random_double : RandomDataGeneratorTestBase
    {
        private const double ExpectedDouble = 22.5;

        private Establish Context = () =>
                                        {
                                            Random.Stub( x => x.NextDouble() ).Return( ExpectedDouble );
                                            Random.Stub( x => x.Next() ).Return( 1 );
                                        };

        private Because Of = () => _randomDouble = RandomDataGeneratorUnderTest.Double;

        private It Should_be_of_type_random_double
            = () => _randomDouble.ShouldBeOfType< RandomData.RandomDouble >();

        private It Should_return_expected_double_when_calling_value
            = () => ( (RandomData.RandomDouble)_randomDouble ).Value.ShouldEqual( ExpectedDouble );

        private It Should_implicitly_cast_to_double
            = () =>
                  {
                      double value = (RandomData.RandomDouble)_randomDouble;
                      value.ShouldEqual( ExpectedDouble );
                  };

        private static Object _randomDouble;
    }
}