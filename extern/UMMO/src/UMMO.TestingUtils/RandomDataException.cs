using System;

namespace UMMO.TestingUtils
{
    /// <summary>
    /// An exception thrown by the RandomData classes.
    /// </summary>
    public class RandomDataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomDataException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RandomDataException( string message ) : base(message) {}

        // TODO: Uncomment these if we need them.
        //public RandomDataException() {}
        //public RandomDataException(string message, Exception innerException) : base(message, innerException) {}
        //public RandomDataException( SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}