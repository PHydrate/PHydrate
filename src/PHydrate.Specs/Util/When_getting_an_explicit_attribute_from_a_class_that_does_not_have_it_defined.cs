#region Copyright

// This file is part of PHydrate.
// 
// PHydrate is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// PHydrate is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with PHydrate.  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright 2010, Stephen Michael Czetty
// 

#endregion

using Machine.Specifications;
using PHydrate.Attributes;
using PHydrate.Util;

namespace PHydrate.Specs.Util
{
    [ Subject( typeof(TypeExtensions) ) ]
    public class When_getting_an_explicit_attribute_from_a_class_that_does_not_have_it_defined :
        TypeExtensionsSpecificationBase
    {
        private Because Of = () => ExpectedAttribute = typeof(TestClass).GetAttribute< DeleteUsingAttribute >();

        private It Should_be_null
            = () => ExpectedAttribute.ShouldBeNull();
    }
}