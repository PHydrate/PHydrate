using Machine.Specifications;
using Machine.Specifications.Utility;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    public abstract class DataReaderMockSpecsBase
    {
        protected static TestingUtils.DataReaderMock MockUnderTest;

        [UsedImplicitly]
        Establish context = () => { MockUnderTest = new TestingUtils.DataReaderMock(); };
    }
}
