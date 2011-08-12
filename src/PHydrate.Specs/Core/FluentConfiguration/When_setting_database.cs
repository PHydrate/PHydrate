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

using Machine.Specifications;
using PHydrate.Specs.Behaviors;
using Rhino.Mocks;

namespace PHydrate.Specs.Core.FluentConfiguration
{
    [ Subject( typeof(PHydrate.Core.FluentConfiguration) ) ]
    public sealed class When_setting_database : FluentConfigurationSpecificationBase
    {
        private static IDatabaseServiceProvider _databaseServiceProvider;

        private Establish Context =
            () => _databaseServiceProvider = MockRepository.GenerateStub< IDatabaseServiceProvider >();

        private Because Of = () => ReturnedObject = FluentConfiguration.DatabaseProvider( _databaseServiceProvider );

        private Behaves_like< AFluentInterface > a_fluent_interface;
    }
}