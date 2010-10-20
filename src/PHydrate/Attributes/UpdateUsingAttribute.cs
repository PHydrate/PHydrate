using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Annotate a class with the stored procedure name used to update a record.
    /// </summary>
    public class UpdateUsingAttribute : CrudAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUsingAttribute"/> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        public UpdateUsingAttribute(string procedureName) : base(procedureName) { }
    }
}