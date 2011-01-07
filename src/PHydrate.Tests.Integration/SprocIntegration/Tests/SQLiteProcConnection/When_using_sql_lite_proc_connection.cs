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
using System.Data;
using Machine.Specifications;

namespace PHydrate.Tests.Integration.SprocIntegration.Tests.SQLiteProcConnection
{
    [ Subject( typeof(SprocIntegration.SQLiteProcCommand) ) ]
    public sealed class When_using_sql_lite_proc_connection : SQLiteProcConnectionSpecificationBase
    {
        private static object _x;

        private It Should_throw_not_implemented_exception_when_calling_begin_transaction
            =
            () => Catch.Exception( () => ProcConnection.BeginTransaction() ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_calling_begin_transaction_with_an_argument
            = () =>
              Catch.Exception( () => ProcConnection.BeginTransaction( IsolationLevel.Unspecified ) ).ShouldBeOfType
                  < NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_calling_change_database
            = () =>
              Catch.Exception( () => ProcConnection.ChangeDatabase( "" ) ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_calling_close
            = () => Catch.Exception( () => ProcConnection.Close() ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_calling_dispose
            = () => Catch.Exception( () => ProcConnection.Dispose() ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_getting_the_connection_string
            = () =>
              Catch.Exception( () => _x = ProcConnection.ConnectionString ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_getting_the_connection_timeout
            = () =>
              Catch.Exception( () => _x = ProcConnection.ConnectionTimeout ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_getting_the_database
            = () =>
              Catch.Exception( () => _x = ProcConnection.Database ).ShouldBeOfType< NotImplementedException >();

        private It Should_throw_not_implemented_exception_when_setting_the_connection_string
            = () =>
              Catch.Exception( () => ProcConnection.ConnectionString = null ).ShouldBeOfType< NotImplementedException >();
    }
}