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
using Machine.Specifications;
using Machine.Specifications.Annotations;
using PHydrate.Attributes;
using PHydrate.Core;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.Session
{
    public abstract class SessionSpecificationBase
    {
        protected static ISession SessionUnderTest;
        protected static IDatabaseService DatabaseService;
        protected static IList< TestObject > RequestedObjects;
        private static DataReaderMock _dataReaderMock;

        #region Test Classes

        #region Nested type: TestObject

        [ HydrateUsing( "TestStoredProcedure" ) ]
        protected class TestObject
        {
            public int Key { get; set; }
        }

        #endregion

        #region Nested type: TestObjectNoHydrator

        [UsedImplicitly]
        protected class TestObjectNoHydrator
        {
            public int Key { get; set; }
        }

        #endregion

        #region Nested type: TestObjectExplicitHydrator

        [HydrateUsing("TestStoredProcedure")]
        [ObjectHydrator(typeof(TestObjectHydrator))]
        protected class TestObjectExplicitHydrator
        {
            public int Key { get; set; }
        }

        #endregion

        #region Nested type: TestObjectHydrator

        private class TestObjectHydrator : IObjectHydrator<TestObjectExplicitHydrator>
        {
            #region Implementation of IObjectHydrator<TestObjectExplicitHydrator>

            public TestObjectExplicitHydrator Hydrate(IDictionary<string, object> columnValues)
            {
                return new TestObjectExplicitHydrator { Key = (int)columnValues["Key"] };
            }

            #endregion
        }

        #endregion

        #region Nested type: TestObjectExplicitHydrator

        [HydrateUsing("TestStoredProcedure")]
        [ObjectHydrator(typeof(TestObjectHydratorNoDefaultConstructor))]
        protected class TestObjectExplicitHydratorNoDefaultConstructor
        {
            public int Key { get; set; }
        }

        #endregion

        #region Nested type: TestObjectHydrator

        private class TestObjectHydratorNoDefaultConstructor : IObjectHydrator<TestObjectExplicitHydrator>
        {
            [CoverageExclude]
            public TestObjectHydratorNoDefaultConstructor([UsedImplicitly]int foo)
            {
            }

            #region Implementation of IObjectHydrator<TestObjectExplicitHydrator>

            [CoverageExclude]
            public TestObjectExplicitHydrator Hydrate(IDictionary<string, object> columnValues)
            {
                return new TestObjectExplicitHydrator { Key = (int)columnValues["Key"] };
            }

            #endregion
        }

        #endregion


        #endregion

        [ UsedImplicitly ]
        private Establish Context = () => {
                                        _dataReaderMock = A.DataReader as DataReaderMock;
                                        _dataReaderMock.AddRecordSet( "Key" );
                                        _dataReaderMock.AddRow( 1 );
                                        _dataReaderMock.AddRow( 2 );
                                        _dataReaderMock.Playback();

                                        DatabaseService = MockRepository.GenerateStub< IDatabaseService >();
                                        DatabaseService.Expect( x => x.ExecuteStoredProcedureReader( "", null ) ).
                                            Constraints( Is.Equal( "TestStoredProcedure" ), Is.NotNull() ).Return(
                                                _dataReaderMock );

                                        SessionUnderTest = new PHydrate.Core.Session( DatabaseService,
                                                                                      new PHydrate.Core.
                                                                                          DefaultObjectHydrator() );
                                    };

        protected static void AssertDatabaseServiceParameter( string parameterName, int parameterValue )
        {
            var parameters =
                (IDictionary< string, object >)
                DatabaseService.GetArgumentsForCallsMadeOn( x => x.ExecuteStoredProcedureReader( "", null ) )[ 0 ][
                    parameterValue ];
            parameters.Keys.ShouldContain( parameterName );
            parameters[ parameterName ].ShouldEqual( parameterValue );
        }
    }
}