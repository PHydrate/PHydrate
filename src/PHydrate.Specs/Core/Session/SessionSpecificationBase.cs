using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using PHydrate.Attributes;
using PHydrate.Core;
using Rhino.Mocks;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.Session
{
    public abstract class SessionSpecificationBase {
        protected static ISession SessionUnderTest;
        protected static IDatabaseService DatabaseService;
        protected static DataReaderMock DataReaderMock;

        #region TestObject

        [ HydrateUsing( "TestStoredProcedure" ) ]
        [ CreateUsing( "TestCreateStoredProcedure" ) ]
        protected class TestObject
        {
            public int Key { get; set; }
        }

        #endregion

        [ UsedImplicitly ]
        private Establish Context = () => {
                                        DataReaderMock = A.DataReader as DataReaderMock;
                                        DatabaseService = MockRepository.GenerateStub< IDatabaseService >();
                                        SessionUnderTest = new PHydrate.Core.Session( DatabaseService,
                                                                                      new PHydrate.Core.
                                                                                          DefaultObjectHydrator(), "@" );
                                    };

        protected static void AssertDatabaseServiceParameter( string parameterName, int parameterValue, Action< IDatabaseService > action )
        {
            var parameters =
                (IEnumerable<KeyValuePair< string, object >>)
                DatabaseService.GetArgumentsForCallsMadeOn( action )[ 0 ][ 1 ];
            // TODO: Add ShouldContainItemMatching to MSpec
            parameters.Where( x => x.Key == parameterName ).FirstOrDefault().ShouldNotBeNull();
            parameters.Where( x => x.Key == parameterName ).FirstOrDefault().Value.ShouldEqual( parameterValue );
        }

        [UsedImplicitly]
        protected class TestObjectNoHydrator
        {
            public int Key { get; set; }
        }
    }
}