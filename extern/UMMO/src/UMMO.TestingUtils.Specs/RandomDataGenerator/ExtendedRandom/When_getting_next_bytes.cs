using System;
using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.ExtendedRandom
{
    [Subject(typeof(RandomData.ExtendedRandom))]
    public class When_getting_next_bytes
    {
        private Establish Context = () =>
                                        {
                                            _randomData = new RandomData.ExtendedRandom();
                                            _byteCount = A.Random.Integer.Between( 1, 100 );
                                        };

        private Because Of = () => _bytes = _randomData.NextBytes( _byteCount );

        private It Should_return_non_null_value
            = () => _bytes.ShouldNotBeNull();

        private It Should_return_byte_array
            = () => _bytes.ShouldBeOfType< byte[] >();

        private It Should_return_byte_array_of_length_specified
            = () => ((byte[])_bytes).Length.ShouldEqual( _byteCount );

        private static RandomData.ExtendedRandom _randomData;
        private static int _byteCount;
        private static Object _bytes;
    }
}