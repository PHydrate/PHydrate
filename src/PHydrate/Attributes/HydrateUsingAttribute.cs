using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Annotate a class with the stored procedure used to get an object
    /// </summary>
    public class HydrateUsingAttribute : CrudAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydrateUsingAttribute"/> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        public HydrateUsingAttribute(string procedureName) : base(procedureName) { }
    }
}