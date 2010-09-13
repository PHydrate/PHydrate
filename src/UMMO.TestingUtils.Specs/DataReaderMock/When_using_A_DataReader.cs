using System;
using System.Data;
using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    [Subject(typeof(A))]
    public class When_using_a_data_reader
    {
        private Because Of = () => _mockObject = A.DataReader;

        private It Should_be_of_type_data_reader_mock
            = () => _mockObject.ShouldBeOfType< TestingUtils.DataReaderMock >();

        private static IDataReader _mockObject;
    }
}