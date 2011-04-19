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

using Machine.Specifications;
using Machine.Specifications.Annotations;
using PHydrate.Core;
using Rhino.Mocks;

namespace PHydrate.Specs.Core.SessionFactory
{
    public abstract class SessionFactorySpecificationBase
    {
        protected static ISessionFactory SessionFactoryUnderTest;
        private static IDatabaseService _databaseService;

        [ UsedImplicitly ]
        private Establish Context = () => {
                                        _databaseService = MockRepository.GenerateStub< IDatabaseService >();
                                        SessionFactoryUnderTest = new PHydrate.Core.SessionFactory( _databaseService,
                                                                                                    "@", null );
                                    };
    }
}