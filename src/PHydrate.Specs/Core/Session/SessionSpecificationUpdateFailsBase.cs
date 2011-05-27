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
using Machine.Specifications.Annotations;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace PHydrate.Specs.Core.Session
{
    public abstract class SessionSpecificationUpdateFailsBase : SessionSpecificationUpdateBase
    {
        [ UsedImplicitly ]
        private Establish Context = () =>
                                    DatabaseService.Expect( x => x.ExecuteStoredProcedureScalar< long >( "", null ) )
                                        .
                                        Constraints( Is.Equal( "TestUpdateStoredProcedure" ), Is.NotNull() ).Return(
                                            0 );
    }
}