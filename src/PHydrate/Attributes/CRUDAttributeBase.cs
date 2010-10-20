using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Base class for all CRUD attributes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class CrudAttributeBase : Attribute
    {
        /// <summary>
        /// Gets or sets the procedure name.
        /// </summary>
        /// <value>The procedure name.</value>
        public string ProcedureName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudAttributeBase"/> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        protected CrudAttributeBase(string procedureName)
        {
            ProcedureName = procedureName;
        }
    }
}