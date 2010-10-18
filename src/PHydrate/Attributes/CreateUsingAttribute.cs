using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Annotate a class with the stored procedure used to create a new record
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CreateUsingAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the procedure name.
        /// </summary>
        /// <value>The procedure name.</value>
        public string ProcedureName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUsingAttribute"/> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        public CreateUsingAttribute(string procedureName)
        {
            ProcedureName = procedureName;
        }
    }
}