using System;

namespace UMMO.Extensions
{
    /// <summary>
    /// Interface for exception logging
    /// </summary>
    public interface ILogWrapper : IDisposable
    {
        /// <summary>
        /// Logs an exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void LogException( Exception exception );
    }
}