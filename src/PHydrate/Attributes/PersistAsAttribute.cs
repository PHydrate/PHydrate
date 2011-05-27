#region Copyright

// This file is part of PHydrate.
// 
// PHydrate is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// PHydrate is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with PHydrate.  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright 2010-2011, Stephen Michael Czetty

#endregion

using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Specify the name of the column that the member uses
    /// </summary>
    /// <remarks>
    /// This attribute is currently ignored, please do not use yet!
    /// </remarks>
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
        public PersistAsAttribute( string columnName )
        {
            ColumnName = columnName;
        }
    }
}