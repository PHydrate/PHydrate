using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PHydrate.Core
{
    /// <summary>
    /// Default hydrator
    /// </summary>
    internal class DefaultObjectHydrator : IObjectHydrator
    {
        public T Hydrate< T >( IDictionary< string, object > columnValues ) where T : new()
        {
            // TODO: remove new() constraint, and reflect to find a suitable constructor
            var objToHydrate = new T();

            // Go through all the properties and get them from the dictionary argument
            PropertyInfo[] propertySetters = typeof(T).GetProperties( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
            foreach (PropertyInfo pi in propertySetters.Where(pi => columnValues.ContainsKey(pi.Name)))
                pi.SetValue( objToHydrate, columnValues[ pi.Name ], BindingFlags.Public | BindingFlags.NonPublic, null,
                             null, null );

            return objToHydrate;
        }
    }
}