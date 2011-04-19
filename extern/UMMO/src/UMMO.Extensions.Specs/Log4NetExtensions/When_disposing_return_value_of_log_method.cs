using System;
using Machine.Specifications;
using Rhino.Mocks;

namespace UMMO.Extensions.Specs.Log4NetExtensions
{
    [Subject(typeof(Extensions.Log4NetExtensions))]
    public class When_disposing_return_value_of_log_method : Log4NetExtensionsSpecsBase
    {
        private Establish Context = () => ReturnValue = LogStub.LogMethod();

        private Because Of = () => ( (IDisposable)ReturnValue ).Dispose();

        private It Should_call_i_logger_log_when_disposed
            = () => LoggerStub.VerifyAllExpectations();
    }
}