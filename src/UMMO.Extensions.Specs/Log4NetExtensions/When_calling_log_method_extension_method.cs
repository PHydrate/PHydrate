using System;
using Machine.Specifications;
using Rhino.Mocks;

namespace UMMO.Extensions.Specs.Log4NetExtensions
{
    [Subject(typeof(Extensions.Log4NetExtensions))]
    public class When_calling_log_method_extension_method : Log4NetExtensionsSpecsBase
    {
        private Because Of = () => ReturnValue = LogStub.LogMethod();

        private It Should_have_called_i_logger_log
            = () => LoggerStub.VerifyAllExpectations();

        private It Should_return_an_i_disposable
            = () => ReturnValue.ShouldBeOfType< IDisposable >();
    }
}