using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PHydrate.Util
{
    /// <summary>
    /// Extensions on generic types
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Gets the data parameters from an instance.
        /// </summary>
        /// <param name="instance">The type.</param>
        /// <param name="parameterPrefix">The parameter prefix.</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetDataParameters<T>(this T instance, string parameterPrefix)
        {
            PropertyInfo[] properties = typeof(T).GetProperties( BindingFlags.Instance | BindingFlags.Public );

            return properties.Select( property => new KeyValuePair< string, object >( parameterPrefix + property.Name,
                                                                                      property.GetValue( instance, null ) ) );
        }
    }
}