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
using System.Collections.Generic;

namespace PHydrate.Core
{
    /// <summary>
    ///   Default hydrator implementation
    /// </summary>
    public interface IDefaultObjectHydrator
    {
        /// <summary>
        ///   Hydrates the specified object type.
        /// </summary>
        /// <typeparam name = "T">The type of object to hydrate</typeparam>
        /// <param name = "columnValues">The column values.</param>
        /// <returns>The hydrated object</returns>
        T Hydrate< T >( IDictionary< string, Object > columnValues );
    }
}