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
                return -1;
            }

            #endregion
        }

        [Subject(typeof(BadNumeric))]
        public class When_comparing_bad_numeric
        {
            private Establish Context = () => _badNumeric = new BadNumeric();

            private It Should_return_negative_1_when_calling_compare_to
                = () => _badNumeric.CompareTo( _badNumeric ).ShouldEqual( -1 );

            private static BadNumeric _badNumeric;
        }

        private class BadRandomNumeric : RandomNumericType<BadNumeric>
        {
            public BadRandomNumeric( IRandom random ) : base( random ) {}

            // For test coverage
#pragma warning disable 612,618
            internal BadRandomNumeric() {}
#pragma warning restore 612,618

            #region Overrides of RandomNumericType<BadNumeric>

            public override BadNumeric Value
            {
                get { return new BadNumeric(); }
            }

            protected override BadNumeric GetBetween( BadNumeric min, BadNumeric max )
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        [Subject(typeof(BadRandomNumeric))]
        public class When_using_bad_random_numeric
        {
            private Establish Context = () => _badRandomNumeric = new BadRandomNumeric();

            private Because Of = () => _badNumeric = _badRandomNumeric.Value;

            private It Should_return_new_bad_numeric_when_calling_value
                =
                () => _badNumeric.ShouldBeOfType< BadNumeric >();

            private It Should_throw_not_implemented_exception_when_calling_between
                =
                () =>
                Catch.Exception( () => _badRandomNumeric.Between( (BadNumeric)_badNumeric, (BadNumeric)_badNumeric ) ).ShouldBeOfType
                    < NotImplementedException >();

            private static BadRandomNumeric _badRandomNumeric;
            private static Object _badNumeric = new BadNumeric();
        }
        #endregion
    }
}