using System;
using System.Collections.Generic;

namespace PHydrate.Core
{
    /// <summary>
    /// Default hydrator implementation
    /// </summary>
    internal interface IObjectHydrator
    {
        /// <summary>
        /// Hydrates the specified object type.
        /// </summary>
        /// <typeparam name="T">The type of object to hydrate</typeparam>
        /// <param name="columnValues">The column values.</param>
        /// <returns>The hydrated object</returns>
        T Hydrate< T >( IDictionary< string, Object > columnValues ) where T : new();
    }
}