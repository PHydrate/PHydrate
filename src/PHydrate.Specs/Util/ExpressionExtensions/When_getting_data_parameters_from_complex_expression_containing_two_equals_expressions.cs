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
using PHydrate.Util;

namespace PHydrate.Specs.Util.ExpressionExtensions
{
    [ Subject( typeof(PHydrate.Util.ExpressionExtensions) ) ]
    public class When_getting_data_parameters_from_complex_expression_containing_two_equals_expressions :
        ExpressionExtenionsSpecificationBase
    {
        private static IDictionary< string, object > _dictionary;

        private Establish Context =
            () => ExpressionToTest = ( TestClass x ) => x.TestKey1 == RandomInteger1 && x.TestKey2 == RandomInteger2;

        private Because Of = () => _dictionary = ExpressionToTest.GetDataParameters("@");

        private It Should_contain_correct_value_in_testkey1
            = () => _dictionary[ "@TestKey1" ].ShouldEqual( RandomInteger1 );

        private It Should_contain_correct_value_in_testkey2
            = () => _dictionary[ "@TestKey2" ].ShouldEqual( RandomInteger2 );

        private It Should_contain_key_named_testkey1
            = () => _dictionary.Keys.ShouldContain( "@TestKey1" );

        private It Should_contain_key_named_testkey2
            = () => _dictionary.Keys.ShouldContain( "@TestKey2" );

        private It Should_return_dictionary
            = () => _dictionary.ShouldNotBeNull();

        private It Should_return_two_entries
            = () => _dictionary.Count.ShouldEqual( 2 );
    }
}