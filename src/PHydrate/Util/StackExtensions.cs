using System;
using System.Collections.Generic;

namespace PHydrate.Util
{
    /// <summary>
    /// Extension methods for <see cref="Stack{T}"/>
    /// </summary>
    public static class StackExtensions
    {
        /// <summary>
        /// Try to pop a value from the stack, return default(T) if empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack">The stack.</param>
        /// <returns></returns>
        public static T TryPop<T>(this Stack<T> stack)
        {
            try
            {
                T transaction;
                lock (stack)
                    transaction = stack.Pop();
                return transaction;
            }
            catch (InvalidOperationException)
            {
                return default(T);
            }
        }
    }
}