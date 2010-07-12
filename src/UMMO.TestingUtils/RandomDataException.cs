using System;
using System.Runtime.Serialization;

namespace UMMO.TestingUtils
{
    public class RandomDataException : Exception
    {
        public RandomDataException( string message ) : base(message) {}
        public RandomDataException() {}
        public RandomDataException(string message, Exception innerException) : base(message, innerException) {}
        public RandomDataException( SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}