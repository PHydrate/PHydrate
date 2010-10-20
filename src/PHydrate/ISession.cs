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
// Copyright 2010, Stephen Michael Czetty
// 

#endregion

using System;

namespace PHydrate
{
    /// <summary>
    ///   A single-use session.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        ///   Gets the transaction.
        /// </summary>
        /// <value>The transaction.</value>
        ITransaction Transaction { get; }

        /// <summary>
        ///   Gets an object of type <typeparamref name = "T" /> given the arguments in the query.
        /// </summary>
        /// <typeparam name = "T">The type of object to return.</typeparam>
        /// <param name = "query">The parameters used to select the object.</param>
        /// <returns>The found object, or null if not found.</returns>
        T Get< T >( Action< T > query );

        /// <summary>
        ///   Gets an object of type <typeparamref name = "T" /> given the arguments in the query.
        /// </summary>
        /// <typeparam name = "T">The type of object to return.</typeparam>
        /// <param name = "parameters">The parameters used to select the object.</param>
        /// <returns>The found object, or null if not found.</returns>
        T Get< T >( Object parameters );

        /// <summary>
        ///   Persists the specified object.
        /// </summary>
        /// <typeparam name = "T">The type of the object to persist.</typeparam>
        /// <param name = "objectToPersist">The object to persist.</param>
        void Persist< T >( T objectToPersist );
    }
}