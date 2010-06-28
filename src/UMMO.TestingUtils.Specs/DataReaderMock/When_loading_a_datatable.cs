using System.Data;
using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [Subject(typeof(TestingUtils.DataReaderMock))]
    public class When_loading_a_datatable : DataReaderMockSpecsBase
    {
        private static string _expectedValue = A.Random.Password;
        private static string ColumnName;
        private static readonly DataTable _result = new DataTable();

        private Establish Context = () =>
                                        {
                                            ColumnName = A.Random.Noun;
                                            MockUnderTest.AddRecordSet( ColumnName );
                                            _expectedValue = A.Random.FirstName;
                                            MockUnderTest.AddRow( _expectedValue ).Playback();
                                        };

        private Because Of = () => _result.Load( MockUnderTest );

        private It Should_successfully_load_the_datatable
            = () => _result.IsInitialized.ShouldBeTrue();

        private It Should_contain_the_expected_column
            = () => _result.Columns[ 0 ].ColumnName.ShouldEqual( ColumnName );

        private It Should_contain_the_expected_value
            = () => _result.Rows[ 0 ][ 0 ].ShouldEqual( _expectedValue );
    }
}