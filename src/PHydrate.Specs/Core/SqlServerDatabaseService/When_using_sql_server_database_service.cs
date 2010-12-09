using System;
using System.Data.SqlClient;
using Machine.Specifications;

namespace PHydrate.Specs.Core.SqlServerDatabaseService
{
    [ Subject( typeof(PHydrate.Core.SqlServerDatabaseService) ) ]
    public class When_using_sql_server_database_service
    {
        private Establish Context =
            () => _databaseService = new PHydrate.Core.SqlServerDatabaseService("Data Source=localhost;Initial Catalog=databasethatdoesntexist;Connection Timeout=1");

        private Because Of =
            () => _exception = Catch.Exception( () => _databaseService.ExecuteStoredProcedureReader( "" ) );

        private It Should_throw_exception_of_type_sql_exception
            = () => _exception.ShouldBeOfType< SqlException >();

        private static PHydrate.Core.SqlServerDatabaseService _databaseService;
        private static Exception _exception;
    }
}