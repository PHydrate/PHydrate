using System;
using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock) )]
    public class When_querying_datareader_metadata : DataReaderMockSpecsWithRecordSetDefined
    {
        Because of = () => MockUnderTest.Playback();

        It should_return_one_when_fieldcount_is_called
            = () => MockUnderTest.FieldCount.ShouldEqual( 1 );

        It should_return_zero_when_getordinal_is_called_with_columnname
            = () => MockUnderTest.GetOrdinal( ColumnName ).ShouldEqual( 0 );

        It should_throw_exception_when_getordinal_is_called_with_unknown_columnname
            = () =>
              typeof(IndexOutOfRangeException).ShouldBeThrownBy( () => MockUnderTest.GetOrdinal( A.Random.LastName ) );
    }
}
