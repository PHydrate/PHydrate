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

    [Subject(typeof(RandomDecimal))]
    public class When_getting_random_decimal_between_two_values
    {
        private Establish Context = () =>
                                        {
                                            _randomDecimal = A.Random.Decimal;
                                            _minValue = A.Random.Decimal;
                                            _maxValue = A.Random.Decimal.GreaterThan( _minValue );
                                        };

        private Because Of = () => _randomValue = _randomDecimal.Between( _minValue, _maxValue );

        private It Should_be_greater_than_or_equal_to_minvalue
            = () => _randomValue.ShouldBeGreaterThanOrEqualTo( _minValue );

        private It Should_be_less_than_or_equal_to_maxvalue
            = () => _randomValue.ShouldBeLessThanOrEqualTo( _maxValue );

        private static RandomDecimal _randomDecimal;
        private static decimal _minValue;
        private static decimal _maxValue;
        private static decimal _randomValue;
    }

    [Subject(typeof(RandomDecimal))]
    public class When_getting_random_decimal_greater_than_negative_number
    {
        private Establish Context = () => _minValue = A.Random.Decimal.LessThan( 0m );

        private Because Of = () => _actualValue = A.Random.Decimal.GreaterThan( _minValue );

        private It Should_return_value_greater_than_or_equal_to_min_value
            = () => _actualValue.ShouldBeGreaterThanOrEqualTo( _minValue );

        private static decimal _minValue;
        private static decimal _actualValue;
    }

    [Subject(typeof(RandomDecimal))]
    public class When_getting_random_decimal_less_than_positive_number
    {
        private Establish Context = () => _maxValue = A.Random.Decimal.GreaterThan(0m);

        private Because Of = () => _actualValue = A.Random.Decimal.LessThan(_maxValue);

        private It Should_return_value_less_than_or_equal_to_min_value
            = () => _actualValue.ShouldBeLessThanOrEqualTo(_maxValue);

        private static decimal _maxValue;
        private static decimal _actualValue;
    }

    [Subject( typeof(RandomDecimal))]
    public class When_getting_random_decimal_between_two_values_in_incorrect_order
    {
        private Establish Context = () =>
                                        {
                                            _randomDecimal = A.Random.Decimal;
                                            _minValue = A.Random.Decimal;
                                            _maxValue = A.Random.Decimal.LessThan( _minValue - 1.0m );
                                        };

        private Because Of = () => _exception = Catch.Exception( () => _randomDecimal.Between( _minValue, _maxValue ) );

        private It Should_throw_exception_when_minvalue_is_greater_than_maxvalue
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_exception_of_type_argument_exception
            = () => _exception.ShouldBeOfType< ArgumentException >();

        private static RandomDecimal _randomDecimal;
        private static decimal _minValue;
        private static decimal _maxValue;
        private static Exception _exception;
    }

    [Subject(typeof(RandomExtensions))]
    public class When_getting_next_decimal_between_min_and_max_where_min_is_greater_than_max
    {
        private Establish Context = () => _random = new Random();

        private It Should_throw_invalid_operation_exception
            = () => Catch.Exception( () => _random.NextDecimal( 1, 0 ) ).ShouldBeOfType< InvalidOperationException >();

        private static Random _random;
    }
}