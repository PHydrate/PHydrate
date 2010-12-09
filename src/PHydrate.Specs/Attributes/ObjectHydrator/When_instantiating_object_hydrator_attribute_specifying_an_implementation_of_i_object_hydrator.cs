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
using PHydrate.Attributes;
using Rhino.Mocks;

namespace PHydrate.Specs.Attributes.ObjectHydrator
{
    [ Subject( typeof(ObjectHydratorAttribute) ) ]
    public class When_instantiating_object_hydrator_attribute_specifying_an_implementation_of_i_object_hydrator
    {
        private static Type _hydratorType;
        private static ObjectHydratorAttribute _objectHydratorAttribute;

        private Establish Context = () => {
                                        var mockObjectHydrator =
                                            MockRepository.GenerateStub< IObjectHydrator< object > >();
                                        _hydratorType = mockObjectHydrator.GetType();
                                    };

        private Because Of = () => _objectHydratorAttribute = new ObjectHydratorAttribute( _hydratorType );

        private It Should_store_type_in_property
            = () => _objectHydratorAttribute.HydratorType.ShouldEqual( _hydratorType );
    }
}