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

#endregion

using System;
using Machine.Specifications;
using PHydrate.Attributes;
using PHydrate.Util;

namespace PHydrate.Specs.Util.GenericExtensions
{
    [ Subject( typeof(PHydrate.Util.GenericExtensions) ) ]
    public sealed class When_setting_an_event_tagged_with_a_specific_attribute
    {
        private static TestObject _dataObject;
        private static Exception _exception;

        #region TestObject

        private class TestObject
        {
            [ UsedImplicitly ]
            public event EventHandler TestEvent;
        }

        #endregion

        private Establish Context = () => _dataObject = new TestObject();

        private Because Of =
            () =>
            _exception =
            Catch.Exception( () => _dataObject.SetPropertyValueWithAttribute< TestObject, UsedImplicitlyAttribute >( 0 ) );

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_phydrate_internal_exception
            = () => _exception.ShouldBeOfType< PHydrateInternalException >();
    }
}