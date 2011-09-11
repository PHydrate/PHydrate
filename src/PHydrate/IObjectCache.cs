namespace PHydrate
{
    /// <summary>
    /// A cache of objects, used by SessionFactory
    /// </summary>
    public interface IObjectCache
    {
        /// <summary>
        /// Determines whether the object exists in the cache
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="hashCode">The hash code.</param>
        /// <returns>
        ///   <c>true</c> the object exists; otherwise, <c>false</c>.
        /// </returns>
        bool IsInCache< T >( int hashCode );

        /// <summary>
        /// Gets from cache.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="hashCode">The hash code.</param>
        /// <returns>The object from the cache</returns>
        T GetFromCache< T >( int hashCode );

        /// <summary>
        /// Adds to the cache.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="hashCode">The hash code.</param>
        /// <param name="obj">The object to store.</param>
        void AddToCache<T>(int hashCode, T obj);

        /// <summary>
        /// Removes old entries from the cache.
        /// </summary>
        void Cleanup();
    }
}