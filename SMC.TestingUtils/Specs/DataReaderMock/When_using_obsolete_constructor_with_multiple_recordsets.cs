using Machine.Specifications;

namespace SMC.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock), "Obsolete constructor" )]
    public class When_using_obsolete_constructor_with_multiple_recordsets : DataReaderMockSpecsUsingObsoleteConstructor
    {
#pragma warning disable 612,618
        Because of = () => MockUnderTest = new TestingUtils.DataReaderMock( RecordSet, RecordSet );
#pragma warning restore 612,618

        It should_contain_two_recordsets
            = () => MockUnderTest.NextResult().ShouldBeTrue();
    }
}
