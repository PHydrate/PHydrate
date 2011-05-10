using System;
using Afterthought;
using PHydrate.Aspects.Logging;

namespace PHydrate.Aspects
{
    /// <summary>
    /// Mark a class as logged.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Assembly)]
    [CLSCompliant(false)]
    public class LoggedAttribute : AmendmentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggedAttribute"/> class.
        /// </summary>
        public LoggedAttribute() : base( typeof(LogAmendment<>) ) {}
    }
}