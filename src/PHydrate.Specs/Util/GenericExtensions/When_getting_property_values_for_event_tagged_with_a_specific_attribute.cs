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
using System.Linq;
using Machine.Specifications;
using PHydrate.Attributes;
using PHydrate.Util;

namespace PHydrate.Specs.Util.GenericExtensions
{
    [ Subject( typeof(PHydrate.Util.GenericExtensions) ) ]
    public class When_getting_property_values_for_event_tagged_with_a_specific_attribute
    {
        #region Test Object

        private class TestObject
        {
            [ UsedImplicitly ]
            public event EventHandler TestEvent;
        }

        #endregion

        private static TestObject _dataObject;
        private static Exception _exception;
        private Establish Context = () => _dataObject = new TestObject();

        // Need to add .ToList() so that the code actually runs.
        private Because Of
            = () => _exception =
                    Catch.Exception(
                        () => _dataObject.GetPropertyValuesWithAttribute< UsedImplicitlyAttribute >().ToList() );

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_phydrate_exception
            = () => _exception.ShouldBeOfType< PHydrateException >();
    }
}