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
// Copyright 2010-2011, Stephen Michael Czetty

#endregion

using System;
using Machine.Specifications;

namespace PHydrate.Specs.Core.DefaultObjectHydrator
{
    [ Subject( typeof(PHydrate.Core.DefaultObjectHydrator) ) ]
    public sealed class When_hydrating_an_object_without_a_parameterless_constructor_or_one_that_matches_the_arguments :
        DefaultObjectHydratorSpecificationBase
    {
        private static Exception _exception;

        #region Test Class

        private class TestHydrationTargetWithExplicitConstructor : TestHydrationTarget
        {
            [ CoverageExclude ]
            public TestHydrationTargetWithExplicitConstructor( int fakeProperty, string anotherFakeProperty ) {}
        }

        #endregion

        private Because Of =
            () =>
            _exception =
            Catch.Exception(
                () => DefaultObjectHydrator.Hydrate< TestHydrationTargetWithExplicitConstructor >( ColumnValues ) );

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_exception_of_type_phydrate_exception
            = () => _exception.ShouldBeOfType< PHydrateException >();
    }
}