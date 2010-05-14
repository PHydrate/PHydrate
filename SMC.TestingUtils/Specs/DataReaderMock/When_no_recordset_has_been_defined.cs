using System;
using Machine.Specifications;

namespace SMC.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock) )]
    public class When_no_recordset_has_been_defined : DataReaderMockSpecsBase
    {
        It should_throw_exception_when_adding_a_row
            = () => typeof(InvalidOperationException).ShouldBeThrownBy( () => MockUnderTest.AddRow( 0 ) );
    }
}
