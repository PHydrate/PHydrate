using System;
using Machine.Specifications;
using Rhino.Mocks;
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    [Subject(typeof(RandomDouble))]
    public class When_getting_random_double : RandomDataGeneratorTestBase
    {
        private const double ExpectedDouble = 22.5;

        private Establish Context = () =>
                                        {
                                            Random.Stub( x => x.NextDouble() ).Return( ExpectedDouble );
                                            Random.Stub( x => x.Next() ).Return( 1 );
                                        };

        private Because Of = () => _randomDouble = RandomDataGeneratorUnderTest.Double;

        private It Should_be_of_type_random_double
            = () => _randomDouble.ShouldBeOfType< RandomDouble >();

        private It Should_return_expected_double_when_calling_value
            = () => ( (RandomDouble)_randomDouble ).Value.ShouldEqual( ExpectedDouble );

        private It Should_implicitly_cast_to_double
            = () =>
                  {
                      double value = (RandomDouble)_randomDouble;
                      value.ShouldEqual( ExpectedDouble );
                  };

        private static Object _randomDouble;
    }
}