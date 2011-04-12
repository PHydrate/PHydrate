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
    ///   Annotate a class with the stored procedure used to create a new record
    /// </summary>
    [ AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = true ) ]
    [ Serializable ]
    public sealed class CreateUsingAttribute : CrudAttributeBase
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "CreateUsingAttribute" /> class.
        /// </summary>
        /// <param name = "procedureName">Name of the procedure.</param>
        public CreateUsingAttribute( string procedureName ) : base( procedureName ) {}
    }
}