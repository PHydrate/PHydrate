#region Copyright

// This file is part of UMMO.
// 
// UMMO is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// UMMO is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with UMMO.  If not, see <http://www.gnu.org/licenses/>.
//  
// Copyright 2010, Stephen Michael Czetty

#endregion

using System;
using log4net;
using log4net.Core;
using Machine.Specifications;
using Rhino.Mocks;

namespace UMMO.Extensions.Specs
{
    public class Log4NetExtensionsSpecsBase
    {
        private Establish Context = () =>
                                        {
                                            LogStub = MockRepository.GenerateStub< ILog >();
                                            LoggerStub = MockRepository.GenerateMock< ILogger >();
                                            LogStub.Stub( x => x.Logger ).Return( LoggerStub );
                                            LoggerStub.Expect( x => x.Log( null, null, null, null ) ).IgnoreArguments();
                                        };

        protected static object ReturnValue;
        protected static ILog LogStub;
        protected static ILogger LoggerStub;
    }

    [Subject(typeof(Log4NetExtensions))]
    public class When_calling_log_method_extension_method : Log4NetExtensionsSpecsBase
    {
        private Because Of = () => ReturnValue = LogStub.LogMethod();

        private It Should_have_called_i_logger_log
            = () => LoggerStub.VerifyAllExpectations();

        private It Should_return_an_i_disposable
            = () => ReturnValue.ShouldBeOfType( typeof(IDisposable) );
    }

    [Subject(typeof(Log4NetExtensions))]
    public class When_disposing_return_value_of_log_method : Log4NetExtensionsSpecsBase
    {
        private Establish Context = () => ReturnValue = LogStub.LogMethod();

        private Because Of = () => ( (IDisposable)ReturnValue ).Dispose();

        private It Should_call_i_logger_log_when_disposed
            = () => LoggerStub.VerifyAllExpectations();
    }
}