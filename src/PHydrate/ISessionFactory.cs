namespace PHydrate
{
    /// <summary>
    /// Factory for getting ISession objects.
    /// </summary>
    /// <remarks>
    /// This class should be instantiated as a singleton.
    /// </remarks>
    public interface ISessionFactory
    {
        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns>An implementation of ISession associated with this factory.</returns>
        ISession GetSession();

        /// <summary>
        /// Gets the global transaction.
        /// </summary>
        /// <value>The global transaction.</value>
        ITransaction GlobalTransaction { get; }
    }
}