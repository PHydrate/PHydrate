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
using PHydrate.Attributes;

namespace PHydrate.Specs.Attributes.ObjectHydrator
{
    [ Subject( typeof(ObjectHydratorAttribute) ) ]
    public class
        When_instantiating_object_hydrator_attribute_specifying_a_class_that_does_not_implement_i_object_hydrator
    {
        private static Exception _exception;
        private Because Of = () => _exception = Catch.Exception( () => new ObjectHydratorAttribute( typeof(Object) ) );

        private It Should_throw_custom_exception
            = () => _exception.ShouldBeOfType( typeof(PHydrateException) );
    }
}