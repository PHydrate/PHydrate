using System;
using Machine.Specifications;
using Rhino.Mocks;

namespace UMMO.Extensions.Specs.Log4NetExtensions
{
    [Subject(typeof(Extensions.Log4NetExtensions))]
    public class When_throwing_exception_from_within_log_using_block : Log4NetExtensionsSpecsBase
    {
        private Because Of = () => _exception = Catch.Exception( TestClass.Test );

        private It Should_call_i_logger_log_when_exception_is_thrown
            = () => LoggerStub.VerifyAllExpectations();

        private It Should_throw_an_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_be_of_type_application_exception
            = () => _exception.ShouldBeOfType<ApplicationException>();

        private static Exception _exception;

        #region Test Class
        private static class TestClass
        {
            [CoverageExclude]
            public static void Test()
            {
                using (LogStub.LogMethod())
                {
                    throw new ApplicationException();
                }
            }
        }

        #endregion
    }
}