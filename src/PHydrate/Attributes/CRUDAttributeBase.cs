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
// 

#endregion

using System;

namespace PHydrate.Attributes
{
    /// <summary>
    ///   Base class for all CRUD attributes
    /// </summary>
    [ AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = true ) ]
    public abstract class CrudAttributeBase : Attribute
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "CrudAttributeBase" /> class.
        /// </summary>
        /// <param name = "procedureName">Name of the procedure.</param>
        protected CrudAttributeBase( string procedureName )
        {
            ProcedureName = procedureName;
        }

        /// <summary>
        ///   Gets or sets the procedure name.
        /// </summary>
        /// <value>The procedure name.</value>
        public string ProcedureName { get; private set; }
    }
}