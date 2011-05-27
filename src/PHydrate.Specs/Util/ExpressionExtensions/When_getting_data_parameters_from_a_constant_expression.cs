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
using Machine.Specifications;
using PHydrate.Util;

namespace PHydrate.Specs.Util.ExpressionExtensions
{
    [ Subject( typeof(PHydrate.Util.ExpressionExtensions) ) ]
    public sealed class When_getting_data_parameters_from_a_constant_expression : ExpressionExtenionsSpecificationBase
    {
        private static IDictionary< string, object > _dictionary;
        private Establish Context = () => ExpressionToTest = ( TestClass x ) => true;

        private Because Of = () => _dictionary = ExpressionToTest.GetDataParameters( "@" );

        private It Should_return_no_items
            = () => _dictionary.Count.ShouldEqual( 0 );
    }
}