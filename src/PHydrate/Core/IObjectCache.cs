using System;

namespace PHydrate.Core
{
    internal interface IObjectCache< in TIdentifierType > : IDisposable
    {
        /// <summary>
        /// Determines whether the object exists in the cache
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="identifier">The identifier.</param>
        /// <returns>
        ///   <c>true</c> the object exists; otherwise, <c>false</c>.
        /// </returns>
        bool IsInCache< T >( TIdentifierType identifier );

        /// <summary>
        /// Gets from cache.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="identifier">The identifier.</param>
        /// <returns>The object from the cache</returns>
        T GetFromCache< T >( TIdentifierType identifier );

        /// <summary>
        /// Adds to the cache.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="identifier">The identifier.</param>
        /// <param name="obj">The object to store.</param>
        void AddToCache<T>(TIdentifierType identifier, T obj);

        /// <summary>
        /// Removes old entries from the cache.
        /// </summary>
        void Cleanup();
    }
}