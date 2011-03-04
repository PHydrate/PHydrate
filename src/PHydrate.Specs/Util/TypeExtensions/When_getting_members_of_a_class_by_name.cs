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
using PHydrate.Util;
using PHydrate.Util.MemberInfoWrapper;

namespace PHydrate.Specs.Util.TypeExtensions
{
    [ Subject( typeof(PHydrate.Util.TypeExtensions) ) ]
    public sealed class When_getting_members_of_a_class_by_name : TypeExtensionsSpecificationBase
    {
        private Because Of =
            () => _memberInfo = typeof(TestClassWithMethod< int >).GetMembersByName( "TestMethodNoArgs" );

        private It Should_not_return_null
            = () => _memberInfo.ShouldNotBeNull();

        private It Should_return_exactly_one_item
            = () => _memberInfo.Count().ShouldEqual( 1 );

        private static IEnumerable< IMemberInfo > _memberInfo;
    }
}