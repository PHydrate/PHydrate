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
// Copyright 2010-2011, Stephen Michael Czetty

#endregion

using System;
using Machine.Specifications;
using PHydrate.Attributes;

namespace PHydrate.Specs.Core.Session
{
    [ Subject( typeof(PHydrate.Core.Session) ) ]
    public sealed class When_getting_an_interface_without_an_object_hydrator_defined : SessionSpecificationBase
    {
        private Because Of =
            () => _exception = Catch.Exception( () => SessionUnderTest.Get< ITestObject >( x => x.Key == 1 ) );

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_phydrate_exception
            = () => _exception.ShouldBeOfType< PHydrateException >();

        private static Exception _exception;

        [ HydrateUsing( "TestProcedure" ) ]
        private interface ITestObject
        {
            int Key { get; }
        }
    }
}