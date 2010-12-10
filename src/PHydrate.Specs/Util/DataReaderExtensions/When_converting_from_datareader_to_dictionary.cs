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

using System.Collections.Generic;
using Machine.Specifications;
using PHydrate.Util;

namespace PHydrate.Specs.Util.DataReaderExtensions
{
    [ Subject( typeof(PHydrate.Util.DataReaderExtensions) ) ]
    public class When_converting_from_datareader_to_dictionary : DataReaderExtensionsSpecificationBase
    {
        private static IDictionary< string, object > _dictionary;
        private Because Of = () => _dictionary = DataReaderToTest.ToDictionary();

        private It Should_have_one_key_named_test_integer
            = () => _dictionary.Keys.ShouldContain( "TestInteger" );

        private It Should_have_one_key_named_test_string
            = () => _dictionary.Keys.ShouldContain( "TestString" );

        private It Should_have_the_correct_value_in_test_integer
            = () => _dictionary[ "TestInteger" ].ShouldEqual( RandomInteger );

        private It Should_have_the_correct_value_in_test_string
            = () => _dictionary[ "TestString" ].ShouldEqual( RandomString );

        private It Should_return_a_dictionary
            = () => _dictionary.ShouldNotBeNull();

        private It Should_return_a_dictionary_containing_two_keys
            = () => _dictionary.Count.ShouldEqual( 2 );
    }
}