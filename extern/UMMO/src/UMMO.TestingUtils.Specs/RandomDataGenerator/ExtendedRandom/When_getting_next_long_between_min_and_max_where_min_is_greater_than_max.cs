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

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.ExtendedRandom
{
    [Subject(typeof(RandomData.ExtendedRandom))]
    public class When_getting_next_long_between_min_and_max_where_min_is_greater_than_max
    {
        private Establish Context = () => _random = new RandomData.ExtendedRandom();

        private Because Of = () => _exception = Catch.Exception(() => _random.NextLong(1, 0));

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_invalid_operation_exception
            = () => _exception.ShouldBeOfType<InvalidOperationException>();

        private static RandomData.ExtendedRandom _random;
        private static Exception _exception;
    }
}