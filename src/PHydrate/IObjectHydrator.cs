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

namespace PHydrate
{
    /// <summary>
    /// Interface enabling a class to hydrate a class of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name = "T">The type this class knows how to hydrate.</typeparam>
    public interface IObjectHydrator< /*out*/ T >
    {
        /// <summary>
        ///   Hydrates the object of type <typeparamref name = "T" />.
        /// </summary>
        /// <param name = "columnValues">The column values from the database.</param>
        /// <returns>The hydrated object</returns>
        T Hydrate( IDictionary< string, Object > columnValues );
    }
}