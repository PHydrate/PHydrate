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

using System.Data;
using Machine.Specifications;
using Rhino.Mocks;

namespace PHydrate.Tests.Integration.SprocIntegration.Tests.SQLiteProcConnection
{
    [ Subject( typeof(SprocIntegration.SQLiteProcConnection) ) ]
    public sealed class When_calling_create_command_on_connection : SQLiteProcConnectionSpecificationBase
    {
        private static IDbCommand _command;
        private Establish Context = () => BaseConnection.Expect( x => x.CreateCommand() );

        private Because Of = () => _command = ProcConnection.CreateCommand();

        private It Should_call_dispose_on_base_command
            = () => BaseConnection.VerifyAllExpectations();

        private It Should_return_command_of_type_sqlite_proc_command
            = () => _command.ShouldBeOfType< SprocIntegration.SQLiteProcCommand >();
    }
}