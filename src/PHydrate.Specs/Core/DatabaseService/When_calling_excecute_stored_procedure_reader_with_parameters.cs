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

using System.Collections.Generic;
using System.Data;
using Machine.Specifications;
using PHydrate.Core;

namespace PHydrate.Specs.Core.DatabaseService
{
    [ Subject( typeof(PHydrate.Core.DatabaseService) ) ]
    public sealed class When_calling_excecute_stored_procedure_reader_with_parameters :
        DatabaseServiceSpecificationReaderBase
    {
        private static IDataReader _dataReader;

        private Because Of =
            () =>
            _dataReader =
            ServiceUnderTest.ExecuteStoredProcedureReader( ProcedureName,
                                                           new Dictionary< string, object > { { "Key", 1 } } );

        private It Should_return_datareader
            = () => _dataReader.ShouldBeTheSameAs( ExpectedDataReader );
    }
}