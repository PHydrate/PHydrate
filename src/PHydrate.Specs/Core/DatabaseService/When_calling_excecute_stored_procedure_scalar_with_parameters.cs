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
// 

#endregion

using System.Collections.Generic;
using Machine.Specifications;
using PHydrate.Core;

namespace PHydrate.Specs.Core.DatabaseService
{
    [ Subject( typeof(DatabaseServiceBase) ) ]
    public class When_calling_excecute_stored_procedure_scalar_with_parameters : DatabaseServiceSpecificationScalarBase
    {
        private static int _actualReturn;

        private Because Of =
            () =>
            _actualReturn =
            ServiceUnderTest.ExecuteStoredProcedureScalar< int >( ProcedureName,
                                                                  new Dictionary< string, object > { { "Key", 1 } } );

        private It Should_return_datareader
            = () => _actualReturn.ShouldEqual( ExpectedInteger );
    }
}