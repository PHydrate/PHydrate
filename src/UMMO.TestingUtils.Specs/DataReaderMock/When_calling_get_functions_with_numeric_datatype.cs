using System.Data;
using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [Subject(typeof(TestingUtils.DataReaderMock))]
    public class When_calling_get_functions_with_numeric_datatype : DataReaderMockSpecsWithRecordSetDefined<int>
    {
        private Establish Context =()=> ExpectedValue = A.Random.Integer;

        private It Should_create_datatable_with_correct_data_when_getschematable_is_called
            = () => AssertThatDataTableFromGetSchemaTableIsCorrect( MockUnderTest.GetSchemaTable() );

        private static void AssertThatDataTableFromGetSchemaTableIsCorrect(DataTable schemaTable)
        {
            schemaTable.Columns.Count.ShouldEqual(4);
            schemaTable.Rows.Count.ShouldEqual(1);
            schemaTable.Rows[0]["ColumnName"].ShouldEqual(ColumnName);
            schemaTable.Rows[0]["ColumnOrdinal"].ShouldEqual(0);
            schemaTable.Rows[0]["ColumnSize"].ShouldEqual(sizeof(int));
            schemaTable.Rows[0]["DataType"].ShouldEqual(ExpectedValue.GetType());
        }
    }
}