using System;
using System.Runtime.Serialization;

namespace PHydrate
{
    /// <summary>
    /// Base class for exceptions thrown by PHydrate
    /// </summary>
    public class PHydrateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PHydrateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PHydrateException( string message ) : base( message )
        {
        }
    }
}