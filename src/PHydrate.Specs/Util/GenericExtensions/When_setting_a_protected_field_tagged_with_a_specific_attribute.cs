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

using Machine.Specifications;
using PHydrate.Attributes;
using PHydrate.Util;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Util.GenericExtensions
{
    [ Subject( typeof(PHydrate.Util.GenericExtensions) ) ]
    public class When_setting_a_protected_field_tagged_with_a_specific_attribute
    {
        private static int _intValue;
        private static TestObject _dataObject;

        #region TestObject

        private class TestObject
        {
            [ PrimaryKey ]
            // ReSharper disable MemberCanBePrivate.Local
                protected int _intValue;

            // ReSharper restore MemberCanBePrivate.Local

            public int IntValue
            {
                get { return _intValue; }
            }
        }

        #endregion

        private Establish Context = () => {
                                        _intValue = A.Random.Integer;
                                        _dataObject = new TestObject();
                                    };

        private Because Of =
            () => _dataObject.SetPropertyValueWithAttribute< TestObject, PrimaryKeyAttribute >( _intValue );

        private It Should_set_the_field_in_the_object
            = () => _dataObject.IntValue.ShouldEqual( _intValue );
    }
}