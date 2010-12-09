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

using System.Linq;
using Machine.Specifications;
using Rhino.Mocks;

namespace PHydrate.Specs.Core.Session
{
    [ Subject( typeof(PHydrate.Core.Session) ) ]
    public class When_getting_an_object_with_simple_expression_with_rhs_containing_a_method_call :
        SessionSpecificationHydrateBase
    {
        private Because Of =
            () => RequestedObjects = SessionUnderTest.Get< TestObject >( x => x.Key == Test() ).ToList();

        private It Should_call_stored_procedure
            = () => DatabaseService.VerifyAllExpectations();

        private It Should_call_stored_procedure_with_parameter_named_key
            = () => AssertDatabaseServiceParameter( "@Key", 1, x => x.ExecuteStoredProcedureReader( "", null ) );

        private It Should_not_be_null
            = () => RequestedObjects.ShouldNotBeNull();

        private It Should_return_correct_record
            = () => RequestedObjects[ 0 ].Key.ShouldEqual( 1 );

        private static int Test()
        {
            return 1;
        }
    }
}