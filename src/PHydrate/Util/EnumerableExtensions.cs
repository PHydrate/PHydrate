using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PHydrate.Attributes;

namespace PHydrate.Util
{
    /// <summary>
    /// Extension methods on IEnumerable
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Casts the contents specified enumerable to the specified type.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="typeToCastTo">The type to cast to.</param>
        /// <returns></returns>
        public static object Cast(this IEnumerable enumerable, Type typeToCastTo)
        {
            var helper = typeof(GenericHelper).GetMethod( "GetGenericEnumerable", BindingFlags.Public | BindingFlags.Static ).MakeGenericMethod( typeToCastTo );
            return helper.Invoke( null, new[] { enumerable } );
        }

        /// <summary>
        /// Casts the contents of the enumerable to an IList of the specified type.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="typeToCastTo">The type to cast to.</param>
        /// <returns></returns>
        public static object ToList(this IEnumerable enumerable, Type typeToCastTo)
        {
            var helper =
                typeof(GenericHelper).GetMethod( "GetGenericList", BindingFlags.Public | BindingFlags.Static ).
                    MakeGenericMethod( typeToCastTo );
            return helper.Invoke( null, new[] { enumerable } );
        }

        private static class GenericHelper
        {
            [UsedImplicitly]
            public static IEnumerable<T> GetGenericEnumerable<T>(IEnumerable enumerable)
            {
                return enumerable.Cast< T >();
            }

            [UsedImplicitly]
            public static IList<T> GetGenericList<T>(IEnumerable enumerable)
            {
                return GetGenericEnumerable< T >( enumerable ).ToList();
            }
        }
    }
}