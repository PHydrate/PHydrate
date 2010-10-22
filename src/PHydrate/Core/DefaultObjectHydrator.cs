using System;
using System.Collections.Generic;

namespace PHydrate.Core
{
    /// <summary>
    /// Default hydrator
    /// </summary>
    internal class DefaultObjectHydrator : IObjectHydrator
    {
        public T Hydrate< T >( IDictionary< string, object > columnValues )
        {
            throw new NotImplementedException();
        }
    }
}