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

namespace PHydrate.Tests.Integration.SprocIntegration.Tests.SQLiteProcCommand
{
    [ Subject( typeof(SprocIntegration.SQLiteProcCommand) ) ]
    public sealed class When_using_sql_lite_proc_command : SQLiteProcCommandSpecificationBase
    {
        private static object _x;

        private It Should_throw_not_implemented_exception_when_calling_cancel
            = () => Catch.Exception( () => ProcCommand.Cancel() ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_calling_execute_non_query
            = () => Catch.Exception( () => ProcCommand.ExecuteNonQuery() ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_calling_execute_reader_with_an_argument
            = () => Catch.Exception( () => ProcCommand.ExecuteReader( CommandBehavior.Default ) ).ShouldBeOfType
                        < NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_calling_prepare
            = () => Catch.Exception( () => ProcCommand.Prepare() ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_getting_the_command_timeout
            = () => Catch.Exception( () => _x = ProcCommand.CommandTimeout ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_getting_the_connection
            = () => Catch.Exception( () => _x = ProcCommand.Connection ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_getting_the_transaction
            = () => Catch.Exception( () => _x = ProcCommand.Transaction ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_getting_the_updated_row_source
            = () =>
              Catch.Exception( () => _x = ProcCommand.UpdatedRowSource ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_setting_the_command_timeout
            = () => Catch.Exception( () => ProcCommand.CommandTimeout = 0 ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_setting_the_connection
            = () => Catch.Exception( () => ProcCommand.Connection = null ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_setting_the_transaction
            = () => Catch.Exception( () => ProcCommand.Transaction = null ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_setting_the_updated_row_source
            = () =>
              Catch.Exception( () => ProcCommand.UpdatedRowSource = new UpdateRowSource() ).ShouldBeOfType
                  < NotImplementedException >();
    }
}