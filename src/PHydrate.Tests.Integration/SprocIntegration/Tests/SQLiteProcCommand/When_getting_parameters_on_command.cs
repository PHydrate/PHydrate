﻿#region Copyright

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

namespace PHydrate.Tests.Integration.SprocIntegration.Tests.SQLiteProcCommand
{
    [ Subject( typeof(SprocIntegration.SQLiteProcCommand) ) ]
    public sealed class When_getting_parameters_on_command : SQLiteProcCommandSpecificationBase
    {
        private static IDataParameterCollection _x;

        private Establish Context =
            () =>
            BaseCommand.Expect( x => x.Parameters ).Return( MockRepository.GenerateStub< IDataParameterCollection >() );

        private Because Of = () => _x = ProcCommand.Parameters;

        private It Should_call_get_parameters_on_base_command
            = () => BaseCommand.VerifyAllExpectations();
    }
}