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

namespace PHydrate
{
    /// <summary>
    /// A specification interface that filters results based on a boolean method.
    /// </summary>
    /// <remarks>
    /// When this interface is used, the filtering happens after the database has returned
    /// results.  If possible, you should use <see cref="IDBSpecification{T}"/> instead.
    /// </remarks>
    /// <typeparam name="T">The type this specification accepts</typeparam>
    public interface IExplicitSpecification< /*in*/ T > : ISpecification< T >
    {
        /// <summary>
        /// Determine if an object satifies the specification
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if the object is specified, false otherwise.</returns>
        bool Satisfies( T obj );
    }
}