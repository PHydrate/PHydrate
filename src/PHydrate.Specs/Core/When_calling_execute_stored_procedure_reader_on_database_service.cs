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

namespace PHydrate.Specs.Core
{
    [ Subject( typeof(DatabaseService) ) ]
    public class When_calling_execute_stored_procedure_reader_on_database_service : DatabaseServiceSpecificationBase
    {
        private static IDataReader _returnedDataReader;

        private Because Of =
            () =>
            _returnedDataReader =
            DatabaseServiceUnderTest.ExecuteStoredProcedureReader( "", MockRepository.GenerateStub< IDataParameter >() );

        private It Should_call_all_expected_methods_on_command_object
            = () => Command.VerifyAllExpectations();

        private It Should_call_all_expected_methods_on_connection_object
            = () => Connection.VerifyAllExpectations();

        private It Should_not_be_null
            = () => _returnedDataReader.ShouldNotBeNull();

        private It Should_return_expected_datareader
            = () => _returnedDataReader.ShouldBeTheSameAs( ExpectedDataReader );
    }
}