using System;
using Afterthought;

namespace PHydrate.Aspects.Logging
{
    /// <summary>
    /// Add logging to all methods of a class
    /// </summary>
    /// <typeparam name="T">The type of the class</typeparam>
    [CLSCompliant(false)]
    public class LogAmendment<T> : Amendment<T, ILoggingObject<T>>
    {
        /// <summary>
        /// Amends this instance.
        /// </summary>
        public override void Amend()
        {
            var loggingProperty = new Property< Logger< T > >( "Logger" ) {
                                                                              LazyInitializer =
                                                                                  ( instance, property ) =>
                                                                                  new Logger< T >()
                                                                          };

            ImplementInterface< ILoggingObject< T > >( loggingProperty );
        }

        /// <summary>
        /// Amends the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        public override void Amend(Method method)
        {
            if ( !ShouldApplyLogging( method ) )
                return;

            method.Before( ( instance, m, parameters ) => instance.Logger.BeginMethod( method.Name, parameters ) );
            //method.After( instance => instance.Logger.EndMethod( method.Name ) );
        }

        private static bool ShouldApplyLogging( Method method )
        {
            return true;
        }
    }
}