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
using Machine.Specifications.Annotations;
using Rhino.Mocks;
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils.Specs
{
    public abstract class RandomDataGeneratorTestBase
    {
        protected static RandomDataGenerator RandomDataGeneratorUnderTest;
        protected static Random Random;

        [UsedImplicitly]
        private Establish Context = () =>
                                        {
                                            Random = MockRepository.GeneratePartialMock< Random >();
                                            RandomDataGeneratorUnderTest = new RandomDataGeneratorAccessor( Random );
                                        };

        #region Nested type: RandomDataGeneratorAccessor

        // Used to get at the protected internal constructor of RandomDataGenerator.
        private class RandomDataGeneratorAccessor : RandomDataGenerator
        {
            protected internal RandomDataGeneratorAccessor( Random random ) : base( random ) {}
        }

        #endregion
    }

    [Subject(typeof(RandomDataGenerator))]
    public class When_getting_random_integer : RandomDataGeneratorTestBase
    {
        private Because Of = () => _randomInteger = RandomDataGeneratorUnderTest.Integer;

        private It Should_be_of_type_random_integer
            = () => _randomInteger.ShouldBeOfType< RandomInteger >();

        private static Object _randomInteger;
    }
}