using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Annotate a class with the stored procedure used to get an object
    /// </summary>
    public class HydrateUsingAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the procedure name.
        /// </summary>
        /// <value>The procedure name.</value>
        public string ProcedureName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HydrateUsingAttribute"/> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        public HydrateUsingAttribute(string procedureName)
        {
            ProcedureName = procedureName;
        }
    }
}