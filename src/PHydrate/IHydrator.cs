using System;
using System.Collections.Generic;

namespace PHydrate
{
    /// <summary>
    /// Classes with the ability to hydrate an object.
    /// </summary>
    public interface IHydrator
    {
        /// <summary>
        /// Hydrates the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object to hydrate.</param>
        /// <param name="columnValues">The column values from the database.</param>
        /// <returns></returns>
        Object Hydrate( Type objectType, IDictionary<string, Object> columnValues );
    }

    /// <summary>
    /// Classes with the ability to hydrate an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type this class knows how to hydrate.</typeparam>
    public interface IHydrator<out T> : IHydrator
    {
        /// <summary>
        /// Hydrates the object of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="columnValues">The column values from the database.</param>
        /// <returns></returns>
        T Hydrate(IDictionary<string, Object> columnValues);
    }
}