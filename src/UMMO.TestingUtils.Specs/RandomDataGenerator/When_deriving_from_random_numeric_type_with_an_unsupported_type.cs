using System;
using Machine.Specifications;
using Rhino.Mocks;
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    [Subject(typeof(RandomNumericType<>))]
    public class When_deriving_from_random_numeric_type_with_an_unsupported_type
    {
        private Establish Context = () => _random = MockRepository.GenerateStub< IRandom >();
        private Because Of = () => _exception = Catch.Exception( () => new BadRandomNumeric( _random ) );

        private It Should_throw_random_data_exception
            = () => _exception.ShouldBeOfType< RandomDataException >();


        private static IRandom _random;
        private static Exception _exception;

        #region Test classes

        // TODO: Add tests to cover these classes, too.
        private struct BadNumeric : IComparable<BadNumeric>
        {
            #region Implementation of IComparable<BadNumeric>

            public int CompareTo( BadNumeric other )
            {
                throw new NotImplementedException();
            }

            #endregion
        }
        private class BadRandomNumeric : RandomNumericType<BadNumeric>
        {
            public BadRandomNumeric( IRandom random ) : base( random ) {}

            #region Overrides of RandomNumericType<BadNumeric>

            public override BadNumeric Value
            {
                get { throw new NotImplementedException(); }
            }

            protected override BadNumeric GetBetween( BadNumeric min, BadNumeric max )
            {
                throw new NotImplementedException();
            }

            #endregion
        }
        #endregion
    }
}