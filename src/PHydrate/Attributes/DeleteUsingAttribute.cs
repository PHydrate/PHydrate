using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Annotate a class with the stored procedure name used to delete an instance
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DeleteUsingAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the procedure name.
        /// </summary>
        /// <value>The procedure name.</value>
        public string ProcedureName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUsingAttribute"/> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        public DeleteUsingAttribute(string procedureName)
        {
            ProcedureName = procedureName;
        }
}