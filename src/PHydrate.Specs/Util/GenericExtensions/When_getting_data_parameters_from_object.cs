using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using PHydrate.Util;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Util.GenericExtensions
{
    [Subject(typeof(PHydrate.Util.GenericExtensions))]
    public class When_getting_data_parameters_from_object
    {
        private Establish Context = () => {
                                        _intValue = A.Random.Integer;
                                        _stringValue = A.Random.String;
                                        _dataObject = new TestObject
                                                      { IntValue = _intValue, StringValue = _stringValue };
                                    };

        private Because Of = () => _expectedResults = _dataObject.GetDataParameters( "" ).ToList();

        private It Should_return_list_with_two_entries
            = () => _expectedResults.Count.ShouldEqual( 2 );

        private It Should_have_the_random_integer_in_intvalue
            = () => _expectedResults.ShouldContain( new KeyValuePair< string, object >( "IntValue", _intValue ) );

        private It Should_have_the_random_string_in_stringvalue
            = () => _expectedResults.ShouldContain( new KeyValuePair< string, object >( "StringValue", _stringValue ) );


        private static TestObject _dataObject;
        private static int _intValue;
        private static string _stringValue;
        private static IList< KeyValuePair< string, object > > _expectedResults;

        private class TestObject
        {
            public int IntValue { get; set; }

            public string StringValue { get; set; }
        }
    }
}