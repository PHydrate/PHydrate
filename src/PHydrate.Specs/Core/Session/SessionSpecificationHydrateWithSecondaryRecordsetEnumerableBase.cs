using System.Collections.Generic;
using Machine.Specifications;
using PHydrate.Attributes;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace PHydrate.Specs.Core.Session
{
    public class SessionSpecificationHydrateWithSecondaryRecordsetEnumerableBase : SessionSpecificationHydrateWithSecondaryRecordsetBase
    {
        private Establish Context = () =>
                                    {
                                        DataReaderMock.AddRecordSet("AggregateKey");
                                        DataReaderMock.AddRow(1);
                                        DataReaderMock.AddRow(2);
                                        DataReaderMock.AddRecordSet("AggregateKey", "Key");
                                        DataReaderMock.AddRow(1, 1);
                                        DataReaderMock.AddRow(1, 2);
                                        DataReaderMock.AddRow(2, 3);
                                        DataReaderMock.AddRow(2, 4);
                                        DataReaderMock.AddRow(3, 5);
                                        DataReaderMock.Playback();

                                        DatabaseService.Expect(
                                            x => x.ExecuteStoredProcedureReader(string.Empty, null)).Constraints(
                                                Is.Equal("TestStoredProcedure"), Is.NotNull()).Return(DataReaderMock);
                                    };

        [HydrateUsing("TestStoredProcedure")]
        protected class TestObjectSecondaryRecordsetIEnumerable
        {
            [PrimaryKey]
            public int AggregateKey { get; set; }

            [Recordset(1)]
            public IEnumerable<TestObjectInternal> InnerObjects { get; set; }
        }

        [HydrateUsing("TestStoredProcedure")]
        protected class TestObjectSecondaryRecordsetIList
        {
            [PrimaryKey]
            public int AggregateKey { get; set; }

            [Recordset(1)]
            public IList<TestObjectInternal> InnerObjects { get; set; }
        }
    }
}