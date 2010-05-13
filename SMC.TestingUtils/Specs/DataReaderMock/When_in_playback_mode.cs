using System;
using Machine.Specifications;

namespace SMC.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock) )]
    public class When_in_playback_mode : DataReaderMockSpecsWithRecordSetDefined
    {
        Because of = () => MockUnderTest.Playback();

        It should_return_zero_when_calling_depth
            = () => MockUnderTest.Depth.ShouldEqual( 0 );

        It should_return_zero_when_calling_recordsaffected
            = () => MockUnderTest.RecordsAffected.ShouldEqual( 0 );

        It should_throw_exception_when_adding_a_recordset
            = () => typeof(InvalidOperationException).ShouldBeThrownBy( () => MockUnderTest.AddRecordSet( "test" ) );

        It should_throw_exception_when_adding_a_row
            = () => typeof(InvalidOperationException).ShouldBeThrownBy( () => MockUnderTest.AddRow( 0 ) );
    }
}
