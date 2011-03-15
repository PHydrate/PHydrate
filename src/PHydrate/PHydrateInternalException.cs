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
    ///   An internal PHydrate exception.  This is probably due to a bug in the library itself
    /// </summary>
    [ Serializable ]
    public class PHydrateInternalException : PHydrateException
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "PHydrateInternalException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        public PHydrateInternalException( string message ) : base( message ) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PHydrateInternalException"/> class.
        /// </summary>
        /// <param name="formatMessage">The format string.</param>
        /// <param name="formatValues">The format values.</param>
        [ StringFormatMethod( "formatMessage" ) ]
        public PHydrateInternalException( string formatMessage, params string[] formatValues )
            : this( string.Format( CultureInfo.CurrentCulture, formatMessage, formatValues ) ) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PHydrateInternalException"/> class.
        /// </summary>
        [ UsedImplicitly ]
        public PHydrateInternalException() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PHydrateInternalException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        [ UsedImplicitly ]
        public PHydrateInternalException( string message, Exception innerException ) : base( message, innerException ) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PHydrateInternalException"/> class.
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
        protected PHydrateInternalException( [ NotNull ] SerializationInfo info, StreamingContext context )
            : base( info, context ) {}
    }
}