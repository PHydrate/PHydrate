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

namespace PHydrate.Attributes
{
    /// <summary>
    ///   Specify a custom IObjectHydrator for the class
    /// </summary>
    /// <remarks>
    /// If the type specified with this attribute does not implement <see cref="IObjectHydrator{T}"/>, then a <see cref="PHydrateException"/> will be thrown at runtime.
    /// </remarks>
    [ AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = false ) ]
    public sealed class ObjectHydratorAttribute : Attribute
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "ObjectHydratorAttribute" /> class.
        /// </summary>
        /// <param name = "hydratorType">Type of the hydrator.</param>
        /// <exception cref="PHydrateException">The type specified as an [ObjectHydrator] does not implement <see cref="IObjectHydrator{T}"/>.</exception>
        public ObjectHydratorAttribute( Type hydratorType )
        {
            if ( hydratorType.GetInterface( "PHydrate.IObjectHydrator`1" ) == null )
                throw new PHydrateException(
                    "The type specified as an [ObjectHydrator] does not implement IObjectHydrator<T>" );

            HydratorType = hydratorType;
        }

        /// <summary>
        ///   Gets or sets the type of the hydrator.
        /// </summary>
        /// <value>The type of the hydrator.</value>
        public Type HydratorType { get; private set; }
    }
}