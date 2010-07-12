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

namespace UMMO.TestingUtils.Specs
{
    public class RandomDataGeneratorTests
    {
        protected static RandomDataGenerator RandomDataGeneratorUnderTest;
        protected static Random Random;

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
}