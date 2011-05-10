namespace PHydrate.Aspects.Logging
{
    ///<summary>
    /// Interface for Afterthought to implement when adding logging to a class.
    ///</summary>
    ///<typeparam name="T">The class to be logged</typeparam>
    public interface ILoggingObject<T>
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        Logger<T> Logger { get; }
    }
}