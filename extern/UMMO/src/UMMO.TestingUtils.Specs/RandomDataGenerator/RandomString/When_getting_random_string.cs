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

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomString
{
    [Subject(typeof(RandomData.RandomString))]
    public class When_getting_random_string : RandomStringSpecsBase
    {
        private It Should_be_of_type_random_string
            = () => RandomString.ShouldBeOfType<RandomData.RandomString>();

        private It Should_implicitly_cast_to_string
            = () =>
                  {
                      string value = (RandomData.RandomString)RandomString;
                      value.ShouldNotBeNull();
                  };
    }
}