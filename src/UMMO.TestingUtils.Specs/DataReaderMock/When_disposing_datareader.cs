using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock) )]
    public class When_disposing_datareader : DataReaderMockSpecsBase
    {
        Because of = () =>
                         {
                             MockUnderTest.Playback();
                             MockUnderTest.Dispose();
                         };

        It should_be_closed
            = () => MockUnderTest.IsClosed.ShouldBeTrue();
    }
}
