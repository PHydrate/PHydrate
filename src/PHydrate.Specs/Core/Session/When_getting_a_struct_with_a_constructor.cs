using System.Linq;
using Machine.Specifications;
using PHydrate.Attributes;

namespace PHydrate.Specs.Core.Session
{
    [Subject(typeof(PHydrate.Core.Session))]
    public sealed class When_getting_a_struct_with_a_constructor : SessionSpecificationHydrateBase
    {
        private Because Of =
            () => _returnedValue = SessionUnderTest.Get< TestStruct >( x => x.Key == 1 ).FirstOrDefault();

        private It Should_return_struct
            = () => _returnedValue.ShouldNotBeNull();

        private It Should_set_correct_value
            = () => _returnedValue.Key.ShouldEqual( 1 );

        private It Should_call_constructor
            = () => _returnedValue.TestValue.ShouldEqual( 1 );

        private static TestStruct _returnedValue;

        [HydrateUsing("TestStoredProcedure")]
        private struct TestStruct
        {
            public readonly int Key;
            public readonly int TestValue;

            [UsedImplicitly]
            public TestStruct(int key)
            {
                Key = key;
                TestValue = 1;
            }
        }
    }
}