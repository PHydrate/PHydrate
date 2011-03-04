using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Specify the name of the column that the member uses
    /// </summary>
    [ AttributeUsage( AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true ) ]
    public sealed class PersistAsAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>The name of the column.</value>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistAsAttribute"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        public PersistAsAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}