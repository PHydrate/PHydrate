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

using System.Collections.Generic;
using Machine.Specifications;
using PHydrate.Attributes;
using PHydrate.Util;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Util.GenericExtensions
{
    [ Subject( typeof(PHydrate.Util.GenericExtensions) ) ]
    public class When_getting_property_values_for_members_tagged_with_a_specific_attribute
    {
        #region Test Object

        private class TestObject
        {
            [ PrimaryKey ]
            private int _privateInt;

            public TestObject( int intValue )
            {
                _privateInt = intValue;
            }

            [ PrimaryKey ]
            public int PublicInt { get; set; }
        }

        #endregion

        private static int _publicIntValue;
        private static int _privateIntValue;
        private static TestObject _dataObject;
        private static IEnumerable< object > _values;

        private Establish Context = () => {
                                        _publicIntValue = A.Random.Integer;
                                        _privateIntValue = A.Random.Integer;
                                        _dataObject = new TestObject( _privateIntValue ) { PublicInt = _publicIntValue };
                                    };

        private Because Of = () => _values = _dataObject.GetPropertyValuesWithAttribute< PrimaryKeyAttribute >();

        private It Should_contain_value_for_public_and_private_ints
            = () => _values.ShouldContainOnly( _publicIntValue, _privateIntValue );
    }
}