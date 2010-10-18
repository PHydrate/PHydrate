using System;

namespace PHydrate
{
    /// <summary>
    /// A single-use session.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Gets an object of type <typeparamref name="T"/> given the arguments in the query.
        /// </summary>
        /// <typeparam name="T">The type of object to return.</typeparam>
        /// <param name="query">The parameters used to select the object.</param>
        /// <returns>The found object, or null if not found.</returns>
        T Get< T >( Action< T > query );

        /// <summary>
        /// Gets an object of type <typeparamref name="T"/> given the arguments in the query.
        /// </summary>
        /// <typeparam name="T">The type of object to return.</typeparam>
        /// <param name="parameters">The parameters used to select the object.</param>
        /// <returns>The found object, or null if not found.</returns>
        T Get< T >( Object parameters );

        /// <summary>
        /// Persists the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the object to persist.</typeparam>
        /// <param name="objectToPersist">The object to persist.</param>
        void Persist< T >( T objectToPersist );

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <value>The transaction.</value>
        ITransaction Transaction { get; }
    }
}