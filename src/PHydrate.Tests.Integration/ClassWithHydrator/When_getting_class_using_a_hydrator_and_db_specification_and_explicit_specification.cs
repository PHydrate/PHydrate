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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Machine.Specifications;

namespace PHydrate.Tests.Integration.ClassWithHydrator
{
    [ Subject( typeof(TestDomain.ClassWithHydrator), "Integration" ) ]
    [ Tags( "Integration" ) ]
    public class When_getting_class_using_a_hydrator_and_db_specification_and_explicit_specification :
        PHydrateIntegrationTestBase
    {
        private static IList< TestDomain.ClassWithHydrator > _simpleList;

        private Because Of =
            () => _simpleList = SessionFactory.GetSession().Get( new TestSimpleDbSpecification() ).ToList();

        private It Should_return_zero_records
            = () => _simpleList.Count.ShouldEqual( 0 );

        #region Test Specification Class

        private class TestSimpleDbSpecification : IExplicitSpecification< TestDomain.ClassWithHydrator >,
                                                  IDbSpecification< TestDomain.ClassWithHydrator >
        {
            #region Implementation of IExplicitSpecification<ClassWithHydrator>

            public bool Satisfies( TestDomain.ClassWithHydrator obj )
            {
                return obj.ClassWithHydratorId == 0;
            }

            #endregion

            #region Implementation of IDbSpecification<ClassWithHydrator>

            public Expression< Func< TestDomain.ClassWithHydrator, bool > > Criteria
            {
                get { return x => x.ClassWithHydratorId == 1; }
            }

            #endregion
        }

        #endregion
    }
}