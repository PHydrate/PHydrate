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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Machine.Specifications;
using PHydrate.Specifications;

namespace PHydrate.Tests.Integration.Simple
{
    [ Subject( typeof(TestDomain.Simple), "Integration" ) ]
    [ Tags( "Integration" ) ]
    public sealed class When_getting_a_simple_class_with_a_db_specification_and_explicit_specification :
        PHydrateIntegrationTestBase
    {
        private static IList< TestDomain.Simple > _simpleList;

        private Because Of =
            () => _simpleList = SessionFactory.GetSession().Get( new TestSimpleDbSpecification() ).ToList();

        private It Should_return_zero_records
            = () => _simpleList.Count.ShouldEqual( 0 );

        #region Test Specification Class

        private class TestSimpleDbSpecification : DbSpecification< TestDomain.Simple >
        {
            #region Implementation of DBSpecification<ClassWithHydrator>

            public override Expression< Func< TestDomain.Simple, bool > > Criteria
            {
                get { return x => x.SimpleId == 1; }
            }

            #endregion

            public override bool IsSatisfiedBy( TestDomain.Simple obj )
            {
                return obj.SimpleId == 0;
            }
        }

        #endregion
    }
}