using System.Collections.Generic;

namespace UMMO.Extensions
{
    public static class IDictionaryExtensions
    {
        public static TValue TryGetValue<TKey, TValue>(this IDictionary< TKey, TValue > dictionary, TKey key )
        {
            TValue value;
            return dictionary.TryGetValue( key, out value ) ? value : default( TValue );
        }
    }
}