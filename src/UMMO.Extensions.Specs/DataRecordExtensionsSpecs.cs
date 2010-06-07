using System;
using System.Data;
using Machine.Specifications;
using UMMO.TestingUtils;

namespace UMMO.Extensions.Specs
{
    [Subject(typeof(DataRecordExtensions))]
    public class When_calling_DataRecord_GetValue_extension_method
    {
        private Establish context = () =>
        {
            var record = new DataReaderMock();
            record.AddRecordSet("string", "int", "date", "enum", "nullString", "nullInt", "nullDate", "nullEnum");
            _randomDate = A.Random.DateTime;
            _randomString = A.Random.FirstName;
            _randomInt = A.Random.Integer;
            record.AddRow(_randomString, _randomInt, _randomDate, "X", null, null, null, null);
            record.Playback();
            _dataRecordUnderTest = record;
        };

        private Because of = () => _dataRecordUnderTest.Read();

        private It Should_return_a_string_for_the_string_column
            = () => _dataRecordUnderTest.Value<string>("string").ShouldBeOfType(typeof (string));

        private It Should_be_the_expected_string_value
            = () => _dataRecordUnderTest.Value<string>("string").ShouldEqual(_randomString);

        private It Should_return_an_int_for_the_int_column
            = () => _dataRecordUnderTest.Value<int>("int").ShouldBeOfType(typeof (int));

        private It Should_be_the_expected_int_value
            = () => _dataRecordUnderTest.Value<int>("int").ShouldEqual(_randomInt);

        private It Should_return_an_date_for_the_date_column
            = () => _dataRecordUnderTest.Value<DateTime>("date").ShouldBeOfType(typeof (DateTime));

        private It Should_be_the_expected_date_value
            = () => _dataRecordUnderTest.Value<DateTime>("date").ShouldEqual(_randomDate);

        private It Should_return_an_enum_for_the_enum_column
            = () => _dataRecordUnderTest.Value<TestEnum>("enum").ShouldBeOfType(typeof (TestEnum));

        private It Should_be_the_expected_enum_value
            = () => _dataRecordUnderTest.Value<TestEnum>("enum").ShouldEqual(TestEnum.X);

        private It Should_return_null_for_the_nullstring_column
            = () => _dataRecordUnderTest.Value<string>("nullString").ShouldBeNull();

        private It Should_return_zero_for_the_nullint_column
            = () => _dataRecordUnderTest.Value<int>("nullInt").ShouldEqual(0);

        private It Should_return_the_default_date_for_the_nulldate_column
            = () => _dataRecordUnderTest.Value<DateTime>("nullDate").ShouldEqual(default(DateTime));

        private It Should_return_testenum_none_for_the_nullenum_column
            = () => _dataRecordUnderTest.Value<TestEnum>("nullEnum").ShouldEqual(TestEnum.None);


        private static IDataReader _dataRecordUnderTest;
        private static DateTime _randomDate;
        private static string _randomString;
        private static int _randomInt;

        private enum TestEnum
        {
            None = 0,
            X = 1
        }
    }
}