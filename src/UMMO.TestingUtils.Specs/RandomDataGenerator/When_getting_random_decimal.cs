using System;
using Machine.Specifications;
using Rhino.Mocks;
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    [Subject(typeof(RandomDecimal))]
    public class When_getting_random_decimal : RandomDataGeneratorTestBase
    {
        private const decimal ExpectedDecimal = 22.5m;
        private Establish Context = () => Random.Stub( x => x.NextDecimal() ).Return( ExpectedDecimal );
        private Because Of = () => _randomDecimal = RandomDataGeneratorUnderTest.Decimal;

        private It Should_be_of_type_random_decimal
            = () => _randomDecimal.ShouldBeOfType< RandomDecimal >();

        private It Should_return_expected_decimal_when_calling_value
            = () => ( (RandomDecimal)_randomDecimal ).Value.ShouldEqual( ExpectedDecimal );

        private It Should_implicitly_cast_to_decimal
            = () =>
                  {
                      decimal value = (RandomDecimal)_randomDecimal;
                      value.ShouldEqual( ExpectedDecimal );
                  };

        private static Object _randomDecimal;
    }
}