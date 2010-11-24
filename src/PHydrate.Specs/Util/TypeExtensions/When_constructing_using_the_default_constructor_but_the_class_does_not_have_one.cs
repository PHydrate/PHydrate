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

using System;
using Machine.Specifications;
using PHydrate.Util;

namespace PHydrate.Specs.Util.TypeExtensions
{
    [ Subject( typeof(PHydrate.Util.TypeExtensions) ) ]
    public class When_constructing_using_the_default_constructor_but_the_class_does_not_have_one :
        TypeExtensionsSpecificationBase
    {
        private Because Of =
            () =>
            _exception =
            Catch.Exception( () => ObjectHydrator.Hydrate< TestHydrationTargetWithExplicitConstructor >( ColumnValues ) );

        private It Should_throw_exception_of_type_phydrate_exception
            = () => _exception.ShouldBeOfType< PHydrateException >();

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private static Exception _exception;

        private Because Of =
            () =>
            _exception =
            Catch.Exception(
                () =>
                typeof(TestClassWithNoDefaultConstructor).ConstructUsingDefaultConstructor
                    < TestClassWithNoDefaultConstructor >() );

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_phydrate_internal_exception
            = () => _exception.ShouldBeOfType< PHydrateInternalException >();
    }
}
