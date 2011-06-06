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

using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace PHydrate.Specs.Core.Session
{
    [ Subject( typeof(PHydrate.Core.Session) ) ]
    public sealed class When_getting_an_interface_with_an_object_hydrator_defined : SessionSpecificationHydrateBase
    {
        private Because Of =
            () => _requestedObject = SessionUnderTest.Get< ITestObjectExplicitHydrator >( x => x.Key == 1 ).ToList();

        private It Should_not_return_null
            = () => _requestedObject.ShouldNotBeNull();

        private It Should_return_records
            = () => _requestedObject.Count.ShouldEqual( 2 );

        private It Should_contain_correct_object
            = () => _requestedObject[ 0 ].Key.ShouldEqual( 1 );

        private It Should_be_concrete_implementer_of_interface
            = () => _requestedObject[ 0 ].ShouldBeOfType< TestObjectExplicitHydrator >();

        private static IList< ITestObjectExplicitHydrator > _requestedObject;
    }
}