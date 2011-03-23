using System.Collections.Generic;

namespace UMMO.Extensions
{
    /// <summary>
    /// Extensions on classes that implement IDictionary
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Wrapper around <see cref="IDictionary{TKey,TValue}"/> that returns the default of TValue if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value of the key, or the default for the type if the key does not exist.</returns>
        public static TValue TryGetValue<TKey, TValue>(this IDictionary< TKey, TValue > dictionary, TKey key )
        {
            TValue value;
            return dictionary.TryGetValue( key, out value ) ? value : default( TValue );
        }
    }
}