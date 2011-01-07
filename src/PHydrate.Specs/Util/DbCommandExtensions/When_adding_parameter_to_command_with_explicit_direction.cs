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

using System.Data;
using Machine.Specifications;
using PHydrate.Util;
using Rhino.Mocks;

namespace PHydrate.Specs.Util.DbCommandExtensions
{
    [ Subject( typeof(PHydrate.Util.DbCommandExtensions) ) ]
    public sealed class When_adding_parameter_to_command_with_explicit_direction : DbCommandExtensionsSpecificationBase
    {
        private Because Of =
            () => Command.AddParameter< int? >( ParameterName, ParameterDirection.Output );

        private It Should_call_all_expected_methods_on_command
            = () => Command.VerifyAllExpectations();

        private It Should_call_all_expected_methods_on_parameter
            = () => DataParameter.VerifyAllExpectations();

        private It Should_call_all_expected_methods_on_parameters
            = () => Parameters.VerifyAllExpectations();
    }
}