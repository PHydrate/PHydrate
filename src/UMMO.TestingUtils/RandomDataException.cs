using System;

namespace UMMO.TestingUtils
{
    public class RandomDataException : Exception
    {
        public RandomDataException( string message ) : base(message) {}

        // TODO: Uncomment these if we need them.
        //public RandomDataException() {}
        //public RandomDataException(string message, Exception innerException) : base(message, innerException) {}
        //public RandomDataException( SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}