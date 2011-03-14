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
// Copyright 2010-2011, Stephen Michael Czetty

#endregion

using System;
using System.Globalization;
using System.Runtime.Serialization;
using PHydrate.Attributes;

namespace PHydrate
{
    /// <summary>
    ///   Base class for exceptions thrown by PHydrate
    /// </summary>
    [ Serializable ]
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
        /// <param name = "formatMessage">The format string.</param>
        /// <param name = "formatValues">The format values.</param>
        [ StringFormatMethod( "formatMessage" ) ]
        public PHydrateException( string formatMessage, params string[] formatValues )
            : this( string.Format( CultureInfo.CurrentCulture, formatMessage, formatValues ) ) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PHydrateException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        ///   </exception>
        ///   
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        ///   </exception>
        protected PHydrateException( [ NotNull ] SerializationInfo info, StreamingContext context )
            : base( info, context ) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PHydrateException"/> class.
        /// </summary>
        [ UsedImplicitly ]
        public PHydrateException() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PHydrateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        [ UsedImplicitly ]
        public PHydrateException( string message, Exception innerException ) : base( message, innerException ) {}
    }
}