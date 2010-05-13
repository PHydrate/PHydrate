using Machine.Specifications;

namespace SMC.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock) )]
    public class When_calling_get_functions : DataReaderMockSpecsWithRecordSetDefined
    {
        static string _expectedValue = A.Random.Password;
        Because of = () => SetupTestRecord( _expectedValue );

        It should_return_only_the_number_of_columns_into_the_array_passed_to_getvalues
            = () =>
                  {
                      var array = new object[2];
                      MockUnderTest.GetValues( array );
                      array[0].ShouldEqual( _expectedValue );
                      array[1].ShouldBeNull();
                  };

        It should_return_zero_if_getbytes_is_called_with_a_negative_field_offset
            = () => MockUnderTest.GetBytes( 0, -1, new byte[0], 0, 0 ).ShouldEqual( 0 );

        It should_return_zero_if_getchars_is_called_with_a_negative_field_offset
            = () => MockUnderTest.GetChars( 0, -1, new char[0], 0, 0 ).ShouldEqual( 0 );
    }
}
