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
using System.Data.SqlClient;
using Machine.Specifications;

namespace PHydrate.Specs.Core.SqlServerDatabaseService
{
    [ Subject( typeof(PHydrate.Core.SqlServerDatabaseServiceProvider) ) ]
    public sealed class When_using_sql_server_database_service
    {
        private static PHydrate.Core.SqlServerDatabaseServiceProvider _databaseServiceProvider;
        private static Exception _exception;

        private Establish Context =
            () =>
            _databaseServiceProvider =
            new PHydrate.Core.SqlServerDatabaseServiceProvider(
                "Data Source=localhost;Initial Catalog=databasethatdoesntexist;Connection Timeout=1" );

        private Because Of =
            () => _exception = Catch.Exception( () => _databaseServiceProvider.DatabaseService().ExecuteStoredProcedureReader( "" ) );

        private It Should_throw_exception_of_type_sql_exception
            = () => _exception.ShouldBeOfType< SqlException >();
    }
}