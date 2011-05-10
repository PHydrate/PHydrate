using log4net;
using log4net.Core;

namespace PHydrate.Aspects.Logging
{
    /// <summary>
    /// Class created by amendments for logging
    /// </summary>
    /// <typeparam name="T">The class being logged</typeparam>
    public class Logger<T>
    {
        private readonly ILog _log;
        private readonly Level _loggingLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger&lt;T&gt;"/> class, using the default log level of Debug.
        /// </summary>
        public Logger()
        {
            _log = LogManager.GetLogger( typeof(T) );
            _loggingLevel = Level.Debug;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        public Logger(Level loggingLevel) : this()
        {
            _loggingLevel = loggingLevel;
        }

        /// <summary>
        /// Log the start of the method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        public void BeginMethod(string methodName, object[] parameters)
        {
            Log("Entering method: " + methodName);
            if ( parameters.Length == 0 )
                return;

            Log( "Parameters:" );
            foreach ( object o in parameters )
                Log( o.GetType().Name + ": " + o.ToString() );
        }

        private void Log( string stringToLog )
        {
            _log.Logger.Log( typeof(T), _loggingLevel,  stringToLog, null );
        }

        /// <summary>
        /// Log the end of the method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        public void EndMethod( string methodName )
        {
            Log( "Leaving method: " + methodName );
        }
    }
}