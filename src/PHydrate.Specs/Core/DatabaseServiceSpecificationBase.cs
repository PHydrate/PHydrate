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

using System.Data;
using Machine.Specifications;
using PHydrate.Core;
using Rhino.Mocks;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core
{
    public abstract class DatabaseServiceSpecificationBase
    {
        private Establish Context = () => {
                                        Connection = MockRepository.GenerateStub< IDbConnection >();
                                        Command = MockRepository.GenerateStub< IDbCommand >();

                                        ExpectedDataReader = A.DataReader as DataReaderMock;
                                        ExpectedDataReader.AddRecordSet( "test" );
                                        ExpectedDataReader.AddRow( "test" );
                                        ExpectedDataReader.Playback();

                                        Connection.Expect( x => x.CreateCommand() ).IgnoreArguments().Return( Command );
                                        Command.Stub( x => x.Parameters ).Return(
                                            MockRepository.GenerateStub< IDataParameterCollection >() );

                                        Command.Expect( x => x.ExecuteReader( CommandBehavior.Default ) ).
                                            IgnoreArguments().Return( ExpectedDataReader );
            
                                        DatabaseServiceUnderTest = new DatabaseService( Connection );
                                    };

        protected static IDbConnection Connection;
        protected static DataReaderMock ExpectedDataReader;
        protected static IDbCommand Command;
        protected static DatabaseService DatabaseServiceUnderTest;
    }
}