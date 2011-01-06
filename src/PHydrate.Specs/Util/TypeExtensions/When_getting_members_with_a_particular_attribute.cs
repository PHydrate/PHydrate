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
using System.Reflection;
using Machine.Specifications;
using PHydrate.Attributes;
using PHydrate.Util;

namespace PHydrate.Specs.Util.TypeExtensions
{
    [ Subject( typeof(PHydrate.Util.TypeExtensions) ) ]
    public class When_getting_members_with_a_particular_attribute
    {
        #region TestObject

        private class TestObject
        {
            [ PrimaryKey ]
            private int _testItem1;

            [ PrimaryKey ]
            private int _testItem2;
        }

        #endregion

        private static IEnumerable< MemberInfo > _members;
        private static MemberInfo _memberInfo1;
        private static MemberInfo _memberInfo2;

        private Establish Context = () => {
                                        _memberInfo1 =
                                            typeof(TestObject).GetMember( "_testItem1",
                                                                          BindingFlags.Instance | BindingFlags.NonPublic )
                                                [ 0 ];
                                        _memberInfo2 =
                                            typeof(TestObject).GetMember( "_testItem2",
                                                                          BindingFlags.Instance | BindingFlags.NonPublic )
                                                [ 0 ];
                                    };

        private Because Of = () => _members = typeof(TestObject).GetMembersWithAttribute< PrimaryKeyAttribute >();

        private It Should_contain_member_info_for_both_test_item_1_and_test_item_2
            = () => _members.ShouldContainOnly( _memberInfo1, _memberInfo2 );
    }
}