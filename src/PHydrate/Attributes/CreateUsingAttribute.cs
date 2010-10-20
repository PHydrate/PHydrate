using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Annotate a class with the stored procedure used to create a new record
    /// </summary>
    public class CreateUsingAttribute : CrudAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUsingAttribute"/> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        public CreateUsingAttribute(string procedureName) : base(procedureName) { }
    }
}