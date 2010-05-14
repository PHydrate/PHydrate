using System;
using Machine.Specifications;

namespace SMC.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock), "Obsolete constructor" )]
    public class When_using_obsolete_constructor : DataReaderMockSpecsUsingObsoleteConstructor
    {
#pragma warning disable 612,618
        Because of = () => MockUnderTest = new TestingUtils.DataReaderMock( RecordSet );
#pragma warning restore 612,618

        It should_return_an_object_ready_for_playback
            = () => typeof(InvalidOperationException).ShouldBeThrownBy( () => MockUnderTest.AddRecordSet( "" ) );

        It should_contain_records_with_two_columns
            = () => MockUnderTest.FieldCount.ShouldEqual( 2 );
    }
}
