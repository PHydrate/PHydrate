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
using Machine.Specifications;
using Machine.Specifications.Annotations;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace PHydrate.Specs.Core.Session
{
    [ Subject( typeof(PHydrate.Core.Session) ) ]
    public sealed class
        When_getting_an_object_containing_a_secondary_recordset_using_a_procedure_that_does_not_include_the_second_recordset :
            SessionSpecificationHydrateWithSecondaryRecordsetBase
    {
        [ UsedImplicitly ]
        private Establish Context = () => {
                                        DataReaderMock.AddRecordSet( "AggregateKey", "Key" );
                                        DataReaderMock.AddRow( 1, 1 );
                                        DataReaderMock.AddRow( 2, 2 );
                                        DataReaderMock.Playback();

                                        DatabaseService.Expect(
                                            x => x.ExecuteStoredProcedureReader( string.Empty, null ) ).
                                            Constraints( Is.Equal( "TestStoredProcedure" ), Is.NotNull() ).Return(
                                                DataReaderMock );
                                    };

        private Because Of =
            () => _exception = Catch.Exception( () =>
                                                SessionUnderTest.Get< TestObjectSecondaryRecordset >(
                                                    x => x.AggregateKey == 1 ) );

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_exception_of_type_phydrate_exception
            = () => _exception.ShouldBeOfType< PHydrateException >();

        private static Exception _exception;
    }
}