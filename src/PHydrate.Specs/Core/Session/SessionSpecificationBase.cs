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
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using PHydrate.Attributes;
using PHydrate.Core;
using Rhino.Mocks;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.Session
{
    public abstract class SessionSpecificationBase
    {
        protected static ISession SessionUnderTest;
        protected static IDatabaseService DatabaseService;
        protected static DataReaderMock DataReaderMock;

        #region TestObject

        [ HydrateUsing( "TestStoredProcedure" ) ]
        [ CreateUsing( "TestCreateStoredProcedure" ) ]
        [ UpdateUsing( "TestUpdateStoredProcedure" ) ]
        protected class TestObject
        {
            [ PrimaryKey ]
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

        protected static void AssertDatabaseServiceParameter( string parameterName, int parameterValue,
                                                              Action< IDatabaseService > action )
        {
            var parameters =
                (IEnumerable< KeyValuePair< string, object > >)
                DatabaseService.GetArgumentsForCallsMadeOn( action )[ 0 ][ 1 ];
            // TODO: Add ShouldContainItemMatching to MSpec
            KeyValuePair< string, object > parameter = parameters.Where( x => x.Key == parameterName ).FirstOrDefault();
            parameter.ShouldNotBeNull();
            parameter.Value.ShouldEqual( parameterValue );
        }

        #region Nested type: TestObjectNoHydrator

        [ UsedImplicitly ]
        protected class TestObjectNoHydrator
        {
            public int Key { get; set; }
        }

        #endregion

        #region Nested type: TestObjectNoUpdate

        [ UsedImplicitly ]
        [ CreateUsing( "TestCreateStoredProcedure" ) ]
        protected class TestObjectNoUpdate
        {
            [ PrimaryKey ]
            public int Key { get; set; }
        }

        #endregion
    }
}