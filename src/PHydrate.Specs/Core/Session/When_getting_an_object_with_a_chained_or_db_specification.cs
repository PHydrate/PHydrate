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
using System.Linq;
using Machine.Specifications;
using Rhino.Mocks;

namespace PHydrate.Specs.Core.Session
{
    [ Subject( typeof(PHydrate.Core.Session) ) ]
    public sealed class When_getting_an_object_with_a_chained_or_db_specification : ChainedDbSpecificationBase
    {
        private Because Of =
            () => _exception = Catch.Exception( () =>
                                                RequestedObjects =
                                                SessionUnderTest.Get(
                                                    new TestSpecification1().Or( new TestSpecification2() ) ).ToList() );

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        [ Ignore( "Or in DbSpecifications not supported yet" ) ]
        private It Should_call_stored_procedure
            = () => DatabaseService.VerifyAllExpectations();

        [ Ignore( "Or in DbSpecifications not supported yet" ) ]
        private It Should_not_be_null
            = () => RequestedObjects.ShouldNotBeNull();

        [ Ignore( "Or in DbSpecifications not supported yet" ) ]
        private It Should_return_correct_records
            = () => RequestedObjects.ShouldEachConformTo( x => x.Key == 1 || x.Key == 2 );

        [ Ignore( "Or in DbSpecifications not supported yet" ) ]
        private It Should_return_two_records
            = () => RequestedObjects.Count.ShouldEqual( 2 );

        private static Exception _exception;
    }
}