﻿#region Copyright

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

namespace PHydrate.Specifications
{
    /// <summary>
    /// Base interface for specification types
    /// </summary>
    /// <remarks>
    /// Nothing is implemented by this interface, it is simply necessary
    /// to group other interfaces.
    /// </remarks>
    /// <typeparam name="T">Type this is a specification for</typeparam>
    public interface ISpecification
#if NET40
        < in T >
#else
        < T >
#endif
    {
        /// <summary>
        /// Returns true if the entity passes the specification
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if the entity passes the specification; otherwise, <c>false</c>.
        /// </returns>
        bool IsSatisfiedBy( T entity );
    }
}