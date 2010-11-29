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

using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using PHydrate.Tests.Integration.TestDomain;

namespace PHydrate.Tests.Integration
{
    [ Subject( typeof(Simple) ) ]
    public class When_getting_simple_type : PHydrateIntegrationTestBase
    {
        private static IList< Simple > _simpleList;

        private Because Of =
            () => _simpleList = SessionFactory.GetSession().Get< Simple >( x => x.SimpleId == 1 ).ToList();

        private It Should_populate_integer_value_with_one
            = () => _simpleList[ 0 ].IntegerValue.ShouldEqual( 1 );

        private It Should_populate_simple_id_with_one
            = () => _simpleList[ 0 ].SimpleId.ShouldEqual( 1 );

        private It Should_populate_string_value_with_test
            = () => _simpleList[ 0 ].StringValue.ShouldEqual( "test" );

        private It Should_return_a_single_record
            = () => _simpleList.Count.ShouldEqual( 1 );
    }
}