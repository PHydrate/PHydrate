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
// Copyright 2010, Stephen Michael Czetty

#endregion

using System;

namespace PHydrate.Attributes
{
    /// <summary>
    ///   Specify that this member is loaded from another recordset
    /// </summary>
    [ AttributeUsage( AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true ) ]
    public sealed class RecordsetAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordsetAttribute"/> class.
        /// </summary>
        /// <param name="recordsetNumber">The recordset number.</param>
        public RecordsetAttribute( int recordsetNumber )
        {
            RecordsetNumber = recordsetNumber;
        }

        /// <summary>
        /// Gets the recordset number.
        /// </summary>
        public int RecordsetNumber { get; private set; }
    }
}