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
using System.Data;
using Machine.Specifications;
using Rhino.Mocks;

namespace PHydrate.Tests.Integration.SprocIntegration.Tests.SQLiteProcCommand
{
    [ Subject( typeof(SprocIntegration.SQLiteProcCommand) ) ]
    public sealed class When_calling_execute_reader_on_command_that_does_not_find_a_procedure_to_run :
        SQLiteProcCommandSpecificationBase
    {
        private static Exception _exception;

        private Establish Context = () => {
                                        ProcCommand.CommandType = CommandType.StoredProcedure;
                                        BaseCommand.Expect( x => x.ExecuteReader() ).Return( null );
                                        BaseCommand.Expect( x => x.Parameters ).Return(
                                            MockRepository.GenerateStub< IDataParameterCollection >() );
                                        BaseCommand.Expect( x => x.CreateParameter() ).Return(
                                            MockRepository.GenerateStub< IDbDataParameter >() );
                                    };

        private Because Of = () => _exception = Catch.Exception( () => ProcCommand.ExecuteReader() );

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();
    }
}