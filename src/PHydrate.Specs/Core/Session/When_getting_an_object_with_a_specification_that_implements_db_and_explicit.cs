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
using System.Linq.Expressions;
using Machine.Specifications;
using Rhino.Mocks;

namespace PHydrate.Specs.Core.Session
{
    [ Subject( typeof(PHydrate.Core.Session) ) ]
    public class When_getting_an_object_with_a_specification_that_implements_db_and_explicit :
        SessionSpecificationHydrateBase
    {
        private Because Of = () => RequestedObjects = SessionUnderTest.Get( new TestSpecification() ).ToList();

        private It Should_call_stored_procedure
            = () => DatabaseService.VerifyAllExpectations();

        private It Should_call_stored_procedure_with_parameter_named_key
            = () => AssertDatabaseServiceParameter( "@Key", 1, x => x.ExecuteStoredProcedureReader( "", null ) );

        private It Should_not_be_null
            = () => RequestedObjects.ShouldNotBeNull();

        private It Should_return_correct_record
            = () => RequestedObjects[ 0 ].Key.ShouldEqual( 2 );

        private It Should_return_one_record
            = () => RequestedObjects.Count.ShouldEqual( 1 );

        #region Test Specification Class

        private class TestSpecification : IExplicitSpecification< TestObject >, IDbSpecification< TestObject >
        {
            #region Implementation of IExplicitSpecification<TestObject>

            public bool Satisfies( TestObject obj )
            {
                return obj.Key == 2;
            }

            #endregion

            #region Implementation of IDbSpecification<TestObject>

            public Expression< Func< TestObject, bool > > Criteria
            {
                get { return x => x.Key == 1; }
            }

            #endregion
        }

        #endregion
    }
}