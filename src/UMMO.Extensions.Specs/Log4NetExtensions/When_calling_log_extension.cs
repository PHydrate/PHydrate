using System;
using Machine.Specifications;
using Rhino.Mocks;

namespace UMMO.Extensions.Specs.Log4NetExtensions
{
    [Subject(typeof(Extensions.Log4NetExtensions))]
    public class When_calling_log_extension : Log4NetExtensionsSpecsBase
    {
        private Establish Context = () =>
                                        {
                                            _logger = LogStub.LogMethod();
                                        };

        private Because Of = () => _logger.LogException(new Exception());

        private It Should_call_i_logger_log_when_called
            = () => LoggerStub.VerifyAllExpectations();

        private static ILogWrapper _logger;
    }
}