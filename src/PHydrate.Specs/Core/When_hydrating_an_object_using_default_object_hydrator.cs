using System.Collections.Generic;
using Machine.Specifications;
using PHydrate.Core;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core
{
    [Subject(typeof(DefaultObjectHydrator))]
    public class When_hydrating_an_object_using_default_object_hydrator
    {
        private Establish Context = () => {
                                        _objectHydrator = new DefaultObjectHydrator();
                                        _randomInteger = A.Random.Integer.Value;
                                        _randomString = A.Random.String.Resembling.A.Noun;
                                    };

        private Because Of =
            () =>
            _returnedObject =
            _objectHydrator.Hydrate< TestHydrationTarget >( new Dictionary< string, object > {
                                                                                                 {"IntegerProperty", _randomInteger},
                                                                                                 {"StringProperty", _randomString}
                                                                                             } );

        private It Should_return_object_of_type_test_hydration_target
            = () => _returnedObject.ShouldBeOfType<TestHydrationTarget>();

        private It Should_populate_the_integer_property_correctly
            = () => ( (TestHydrationTarget)_returnedObject ).IntegerProperty.ShouldEqual( _randomInteger );

        private It Should_populate_the_string_property_correctly
            = () => ( (TestHydrationTarget)_returnedObject ).StringProperty.ShouldEqual( _randomString );

        private static IObjectHydrator _objectHydrator;
        private static object _returnedObject;
        private static int _randomInteger;
        private static string _randomString;

        #region Test Hydration Target

        private class TestHydrationTarget
        {
            public int IntegerProperty { get; private set; }

            public string StringProperty { get; private set; }
        }

        #endregion
    }
}