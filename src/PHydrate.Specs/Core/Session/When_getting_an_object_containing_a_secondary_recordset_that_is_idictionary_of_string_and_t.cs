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
using Machine.Specifications;
using PHydrate.Attributes;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace PHydrate.Specs.Core.Session
{
    [ Subject( typeof(PHydrate.Core.Session) ) ]
    public sealed class When_getting_an_object_containing_a_secondary_recordset_that_is_idictionary_of_string_and_t :
        SessionSpecificationHydrateWithSecondaryRecordsetBase
    {
        private Establish Context = () => {
                                        DataReaderMock.AddRecordSet( "AggregateKey" );
                                        DataReaderMock.AddRow( 1 );
                                        DataReaderMock.AddRow( 2 );
                                        DataReaderMock.AddRecordSet( "AggregateKey", "Key" );
                                        DataReaderMock.AddRow( 1, "1" );
                                        DataReaderMock.AddRow( 1, "2" );
                                        DataReaderMock.AddRow( 2, "3" );
                                        DataReaderMock.AddRow( 2, "4" );
                                        DataReaderMock.AddRow( 3, "5" );
                                        DataReaderMock.Playback();

                                        DatabaseService.Expect(
                                            x => x.ExecuteStoredProcedureReader( string.Empty, null ) ).Constraints(
                                                Is.Equal( "TestStoredProcedure" ), Is.NotNull() ).Return( DataReaderMock );
                                    };

        private Because Of =
            () =>
            _requestedObjects =
            SessionUnderTest.Get< TestObjectSecondaryRecordsetIDictionaryWithStringKey >( x => x.AggregateKey == 1 ).
                ToList();

        private It Should_call_stored_procedure
            = () => DatabaseService.VerifyAllExpectations();

        private It Should_call_stored_procedure_with_parameter_named_aggregate_key
            =
            () =>
            AssertDatabaseServiceParameter( "@AggregateKey", 1,
                                            x => x.ExecuteStoredProcedureReader( string.Empty, null ) );

        private It Should_include_correct_internal_records
            = () => _requestedObjects[ 0 ].InnerObjects.Count.ShouldEqual( 2 );

        private It Should_include_internal_record
            = () => _requestedObjects[ 0 ].InnerObjects.ShouldNotBeNull();

        private It Should_not_be_null
            = () => _requestedObjects.ShouldNotBeNull();

        private It Should_return_correct_record
            = () => _requestedObjects[ 0 ].AggregateKey.ShouldEqual( 1 );

        // TODO: Add ShouldContainKeys() to mspec
        private It Should_contain_dictionary_with_expected_keys
            = () =>
              ( _requestedObjects[ 0 ].InnerObjects.ContainsKey( "1" ) &&
                _requestedObjects[ 0 ].InnerObjects.ContainsKey( "2" ) ).ShouldBeTrue();

        private static IList< TestObjectSecondaryRecordsetIDictionaryWithStringKey > _requestedObjects;

        [ HydrateUsing( "TestStoredProcedure" ) ]
        private class TestObjectSecondaryRecordsetIDictionaryWithStringKey
        {
            [ PrimaryKey ]
            public int AggregateKey { get; set; }

            [ Recordset( 1 ) ]
            [ UsedImplicitly ]
            public IDictionary< string, TestObjectInternalStringKey > InnerObjects { get; set; }
        }
    }

    [Subject(typeof(PHydrate.Core.Session))]
    public sealed class When_getting_an_object_containing_a_secondary_recordset_that_is_idictionary_and_has_two_keys :
        SessionSpecificationHydrateWithSecondaryRecordsetBase
    {
        private Establish Context = () =>
        {
            DataReaderMock.AddRecordSet("AggregateKey");
            DataReaderMock.AddRow(1);
            DataReaderMock.AddRow(2);
            DataReaderMock.AddRecordSet("AggregateKey", "Key1", "Key2");
            DataReaderMock.AddRow(1, 1, 1);
            DataReaderMock.AddRow(1, 2, 2);
            DataReaderMock.AddRow(2, 3, 3);
            DataReaderMock.AddRow(2, 4, 4);
            DataReaderMock.AddRow(3, 5, 5);
            DataReaderMock.Playback();

            DatabaseService.Expect(
                x => x.ExecuteStoredProcedureReader(string.Empty, null)).Constraints(
                    Is.Equal("TestStoredProcedure"), Is.NotNull()).Return(DataReaderMock);
        };

        private Because Of =
            () => _exception = Catch.Exception( () =>
                                                _requestedObjects =
                                                SessionUnderTest.Get
                                                    < TestObjectSecondaryRecordsetIDictionaryWithTwoKeys >(
                                                        x => x.AggregateKey == 1 ).
                                                    ToList() );

        private It Should_call_stored_procedure
            = () => DatabaseService.VerifyAllExpectations();

        private It Should_call_stored_procedure_with_parameter_named_aggregate_key
            =
            () =>
            AssertDatabaseServiceParameter("@AggregateKey", 1,
                                            x => x.ExecuteStoredProcedureReader(string.Empty, null));

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_phydrate_exception
            = () => _exception.ShouldBeOfType< PHydrateException >();

        private static IList<TestObjectSecondaryRecordsetIDictionaryWithTwoKeys> _requestedObjects;
        private static Exception _exception;

        [HydrateUsing("TestStoredProcedure")]
        private class TestObjectSecondaryRecordsetIDictionaryWithTwoKeys
        {
            [PrimaryKey]
            public int AggregateKey { get; set; }

            [Recordset(1)]
            [UsedImplicitly]
            public IDictionary<string, TestObjectInternalTwoKeys> InnerObjects { get; set; }
        }
    }
}