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
using PHydrate.Attributes;

namespace PHydrate
{
    /// <summary>
    ///   Base class for exceptions thrown by PHydrate
    /// </summary>
    public class PHydrateException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "PHydrateException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        public PHydrateException( string message ) : base( message ) {}

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PHydrateException" /> class.
        /// </summary>
        /// <param name = "formatString">The format string.</param>
        /// <param name = "formatValues">The format values.</param>
        [ StringFormatMethod( "formatString" ) ]
        public PHydrateException( string formatString, params string[] formatValues )
            : this( string.Format( formatString, formatValues ) ) {}
    }
}