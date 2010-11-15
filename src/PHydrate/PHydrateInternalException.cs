namespace PHydrate
{
    /// <summary>
    /// An internal PHydrate exception.  This is probably due to a bug in the library itself
    /// </summary>
    public class PHydrateInternalException : PHydrateException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PHydrateInternalException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PHydrateInternalException( string message ) : base( message )
        {
        }
    }
}