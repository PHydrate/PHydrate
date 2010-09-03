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

    [Subject(typeof(RandomDouble))]
    public class When_getting_random_double_less_than_positive_number
    {
        private Establish Context = () => _maxValue = A.Random.Double.GreaterThan(0);

        private Because Of = () => _actualValue = A.Random.Double.LessThan(_maxValue);

        private It Should_return_value_less_than_or_equal_to_min_value
            = () => _actualValue.ShouldBeLessThanOrEqualTo(_maxValue);

        private static double _maxValue;
        private static double _actualValue;
    }

    [Subject(typeof(RandomDouble))]
    public class When_getting_random_double_between_two_values_in_incorrect_order
    {
        private Establish Context = () =>
        {
            _randomDouble = A.Random.Double;
            _minValue = A.Random.Double;
            _maxValue = A.Random.Double.LessThan(_minValue - 1.0);
        };

        private Because Of = () => _exception = Catch.Exception(() => _randomDouble.Between(_minValue, _maxValue));

        private It Should_throw_exception_when_minvalue_is_greater_than_maxvalue
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_exception_of_type_argument_exception
            = () => _exception.ShouldBeOfType<ArgumentException>();

        private static RandomDouble _randomDouble;
        private static double _minValue;
        private static double _maxValue;
        private static Exception _exception;
    }
}