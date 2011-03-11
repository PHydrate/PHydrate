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
using System.Linq.Expressions;

namespace PHydrate
{
    /// <summary>
    /// A specification interface for providing a boolean <see cref="Expression"/> to be used to filter results from the database
    /// </summary>
    /// <remarks>
    /// The parsed expression will be used to pass parameters to the stored procedure.
    /// You should only use object members that are supported by the underlying stored procedure.
    /// </remarks>
    /// <typeparam name="T">The type this specification accepts</typeparam>
    public interface IDbSpecification< T > : ISpecification< T >
    {
        /// <summary>
        /// Gets an <see cref="Expression"/> that will be parsed to send parameters to the stored procedure.
        /// </summary>
        Expression< Func< T, bool > > Criteria { get; }
    }
}