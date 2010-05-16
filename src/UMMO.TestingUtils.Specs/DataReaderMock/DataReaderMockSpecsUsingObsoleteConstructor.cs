using System.Collections.Generic;
using Machine.Specifications;
using Machine.Specifications.Utility;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    public abstract class DataReaderMockSpecsUsingObsoleteConstructor
    {
        protected static IList< KeyValuePair< string, object > > RecordSet;
        protected static TestingUtils.DataReaderMock MockUnderTest;
        static string _columnOneName;
        static int _columnOneValue;
        static string _columnTwoName;
        static string _columnTwoValue;

        [UsedImplicitly]
        Establish context = () =>
                                {
                                    _columnOneName = A.Random.FirstName;
                                    _columnOneValue = A.Random.Integer;
                                    _columnTwoName = A.Random.LastName;
                                    _columnTwoValue = A.Random.Password;
                                    RecordSet = new List< KeyValuePair< string, object > >
                                                    {
                                                        new KeyValuePair< string, object >( _columnOneName,
                                                                                            _columnOneValue ),
                                                        new KeyValuePair< string, object >( _columnTwoName,
                                                                                            _columnTwoValue )
                                                    };
                                };
    }
}
