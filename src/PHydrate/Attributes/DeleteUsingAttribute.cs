using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Annotate a class with the stored procedure name used to delete an instance
    /// </summary>
    public class DeleteUsingAttribute : CrudAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUsingAttribute"/> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        public DeleteUsingAttribute(string procedureName) : base(procedureName) { }
    }
}