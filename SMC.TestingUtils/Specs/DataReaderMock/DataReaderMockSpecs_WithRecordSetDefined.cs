using System.Data;
using Machine.Specifications;
using Machine.Specifications.Utility;

namespace SMC.TestingUtils.Specs.DataReaderMock
{
    public abstract class DataReaderMockSpecsWithRecordSetDefined : DataReaderMockSpecsBase
    {
        protected static string ColumnName;

        [UsedImplicitly]
        Establish context = () =>
                                {
                                    ColumnName = A.Random.Noun;
                                    MockUnderTest.AddRecordSet( ColumnName );
                                };

        protected static void AssertThatDataReaderFromGetDataIsCorrect< T >( IDataReader dataReader, T expectedValue )
        {
            dataReader.Read().ShouldBeTrue();
            dataReader[0].ShouldBeOfType( typeof(T) );
            dataReader[0].ShouldEqual( expectedValue );
            dataReader[ColumnName].ShouldBeOfType( typeof(T) );
            dataReader[ColumnName].ShouldEqual( expectedValue );
        }

        protected static void SetupTestRecord< T >( T value )
        {
            MockUnderTest.AddRow( value ).Playback();
            MockUnderTest.Read();
        }

        protected static void AssertThatArrayFromGetValuesIsCorrect< T >( T expectedValue )
        {
            var objArray = new object[1];
            MockUnderTest.GetValues( objArray );
            objArray[0].ShouldNotBeNull();
            objArray[0].ShouldBeOfType( typeof(T) );
            ((T)objArray[0]).ShouldEqual( expectedValue );
        }
    }
}
