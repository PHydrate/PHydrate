﻿#region Copyright

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

#endregion

using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace PHydrate.Specs.Core.Session
{
    [ Subject( typeof(PHydrate.Core.Session) ) ]
    public sealed class When_getting_an_object_with_an_explicit_hydrator : SessionSpecificationHydrateBase
    {
        private static IList< TestObjectExplicitHydrator > _explicitHydratorObjects;

        private Because Of =
            () =>
            _explicitHydratorObjects = SessionUnderTest.Get< TestObjectExplicitHydrator >( x => x.Key == 1 ).ToList();

        private It Should_return_object
            = () => _explicitHydratorObjects.ShouldNotBeNull();

        private It Should_return_two_objects
            = () => _explicitHydratorObjects.Count.ShouldEqual( 2 );
    }
}