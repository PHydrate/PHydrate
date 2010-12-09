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

namespace PHydrate.Specs.Core.DefaultObjectHydrator
{
    [ Subject( typeof(PHydrate.Core.DefaultObjectHydrator) ) ]
    public class When_hydrating_an_object_without_a_parameterless_constructor : DefaultObjectHydratorSpecificationBase
    {
        private Because Of =
            () => ReturnedObject = DefaultObjectHydrator.Hydrate< TestHydrationTargetWithExplicitConstructor >( ColumnValues );

        private It Should_populate_the_integer_property_correctly
            = () => ( (TestHydrationTarget)ReturnedObject ).IntegerProperty.ShouldEqual( RandomInteger );

        private It Should_populate_the_string_property_correctly
            = () => ( (TestHydrationTarget)ReturnedObject ).StringProperty.ShouldEqual( RandomString );

        private It Should_return_object_of_type_test_hydration_target_with_explicit_constructor
            = () => ReturnedObject.ShouldBeOfType< TestHydrationTargetWithExplicitConstructor >();

        #region Test Class

        private class TestHydrationTargetWithExplicitConstructor : TestHydrationTarget
        {
            public TestHydrationTargetWithExplicitConstructor( int integerProperty, string stringProperty )
            {
                IntegerProperty = integerProperty;
                StringProperty = stringProperty;
            }
        }

        #endregion
    }
}