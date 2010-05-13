using Machine.Specifications;

namespace SMC.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock) )]
    public class When_closing_datareader : DataReaderMockSpecsBase
    {
        Because of = () =>
                         {
                             MockUnderTest.Playback();
                             MockUnderTest.Close();
                         };

        It should_be_closed
            = () => MockUnderTest.IsClosed.ShouldBeTrue();

        It should_return_negative_one_from_recordsaffected
            = () => MockUnderTest.RecordsAffected.ShouldEqual( -1 );
    }
}
