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
using Machine.Specifications.Annotations;
using PHydrate.Core;
using Rhino.Mocks;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.DatabaseService
{
    public abstract class DatabaseServiceSpecificationBase
    {
        private static IDbConnection _dbConnection;
        protected static IDbCommand DbCommand;
        protected static string ProcedureName;
        protected static DatabaseServiceBase ServiceUnderTest;
        protected static IDataReader ExpectedDataReader;

        [ UsedImplicitly ]
        private Establish Context = () => {
                                        ProcedureName = A.Random.String;
                                        _dbConnection = MockRepository.GenerateMock< IDbConnection >();
                                        DbCommand = MockRepository.GenerateMock< IDbCommand >();
                                        ExpectedDataReader = MockRepository.GenerateStub< IDataReader >();

                                        _dbConnection.Expect( x => x.CreateCommand() ).Return( DbCommand );
                                        DbCommand.Expect( x => x.CommandType ).SetPropertyWithArgument(
                                            CommandType.StoredProcedure );
                                        DbCommand.Expect( x => x.CommandText ).SetPropertyWithArgument( ProcedureName );
                                        DbCommand.Expect( x => x.ExecuteReader( ) ).
                                            Return( ExpectedDataReader );
                                        DbCommand.Stub( x => x.CreateParameter() ).Return(
                                            MockRepository.GenerateStub< IDbDataParameter >() );
                                        DbCommand.Stub( x => x.Parameters ).Return(
                                            MockRepository.GenerateStub< IDataParameterCollection >() );

                                        ServiceUnderTest = new TestDatabaseService( _dbConnection );
                                    };

        private class TestDatabaseService : DatabaseServiceBase
        {
            private readonly IDbConnection _connection;

            public TestDatabaseService( IDbConnection dbConnection )
            {
                _connection = dbConnection;
            }

            #region Overrides of DatabaseServiceBase

            protected override IDbConnection GetConnection()
            {
                return _connection;
            }

            #endregion
        }
    }
}