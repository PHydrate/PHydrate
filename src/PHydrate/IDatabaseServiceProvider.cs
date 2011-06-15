namespace PHydrate
{
    /// <summary>
    /// Provider for database services
    /// </summary>
    public interface IDatabaseServiceProvider
    {
        /// <summary>
        /// Get an instance of the database service.
        /// </summary>
        /// <returns></returns>
        IDatabaseService DatabaseService();
    }
}