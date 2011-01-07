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
using System.Linq;
using Machine.Specifications;
using PHydrate.Util;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Util.GenericExtensions
{
    [ Subject( typeof(PHydrate.Util.GenericExtensions) ) ]
    public sealed class When_getting_data_parameters_from_object
    {
        private static TestObject _dataObject;
        private static int _intValue;
        private static string _stringValue;
        private static IList< KeyValuePair< string, object > > _expectedResults;

        #region TestObject

        private class TestObject
        {
            public int IntValue { get; set; }

            public string StringValue { get; set; }
        }

        #endregion

        private Establish Context = () => {
                                        _intValue = A.Random.Integer;
                                        _stringValue = A.Random.String;
                                        _dataObject = new TestObject
                                                      { IntValue = _intValue, StringValue = _stringValue };
                                    };

        private Because Of = () => _expectedResults = _dataObject.GetDataParameters( "" ).ToList();

        private It Should_have_the_random_integer_in_intvalue
            = () => _expectedResults.ShouldContain( new KeyValuePair< string, object >( "IntValue", _intValue ) );

        private It Should_have_the_random_string_in_stringvalue
            = () => _expectedResults.ShouldContain( new KeyValuePair< string, object >( "StringValue", _stringValue ) );

        private It Should_return_list_with_two_entries
            = () => _expectedResults.Count.ShouldEqual( 2 );
    }
}