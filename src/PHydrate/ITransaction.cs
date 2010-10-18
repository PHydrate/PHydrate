namespace PHydrate
{
    /// <summary>
    /// Represents a database transaction.
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// Begins this transaction.
        /// </summary>
        void Begin();

        /// <summary>
        /// Commits this transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back this transaction.
        /// </summary>
        void Rollback();
    }
}