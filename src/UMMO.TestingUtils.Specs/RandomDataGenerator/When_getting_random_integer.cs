using System;
using Machine.Specifications;
using Rhino.Mocks;
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    [Subject(typeof(RandomInteger))]
    public class When_getting_random_integer : RandomDataGeneratorTestBase
    {
        private const int ExpectedInteger = 22;
        private Establish Context = () => Random.Stub( x => x.Next() ).Return( ExpectedInteger );
        private Because Of = () => _randomInteger = RandomDataGeneratorUnderTest.Integer;

        private It Should_be_of_type_random_integer
            = () => _randomInteger.ShouldBeOfType< RandomInteger >();

        private It Should_return_expected_integer_when_calling_value
            = () => ( (RandomInteger)_randomInteger ).Value.ShouldEqual( ExpectedInteger );

        private It Should_implicitly_cast_to_integer
            = () =>
                  {
                      int value = (RandomInteger)_randomInteger;
                      value.ShouldEqual( ExpectedInteger );
                  };

        private static Object _randomInteger;
    }
}