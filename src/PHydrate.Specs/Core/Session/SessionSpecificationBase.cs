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

using Machine.Specifications;
using Machine.Specifications.Annotations;
using PHydrate.Attributes;
using PHydrate.Core;
using Rhino.Mocks;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.Session
{
    public class SessionSpecificationBase
    {
        [ UsedImplicitly ]
        private Establish Context = () => {
                                        _dataReaderMock = A.DataReader as DataReaderMock;
                                        _dataReaderMock.AddRecordSet( "Key" );
                                        _dataReaderMock.AddRow( 1 );
                                        _dataReaderMock.Playback();

                                        _databaseService = MockRepository.GenerateStub< IDatabaseService >();
                                        _databaseService.Stub( x => x.ExecuteStoredProcedureReader( "", null ) ).
                                            IgnoreArguments().Return( _dataReaderMock );
                                        SessionUnderTest = new PHydrate.Core.Session( _databaseService );
                                    };

        protected static ISession SessionUnderTest;
        private static IDatabaseService _databaseService;
        protected static TestObject RequestedObject;
        private static DataReaderMock _dataReaderMock;

        #region Test Classes

        #region Nested type: TestObject

        [HydrateUsing("TestStoredProcedure")]
        protected class TestObject
        {
            public int Key { get; set; }
        }

        #endregion

        #region Nested type: TestObjectNoHydrator

        protected class TestObjectNoHydrator
        {
            public int Key { get; set; }
        }

        #endregion

        #endregion
    }
}