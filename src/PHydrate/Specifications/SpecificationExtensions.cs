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

namespace PHydrate.Specifications
{
    /// <summary>
    /// Extension methods for chaining specifications
    /// </summary>
    public static class SpecificationExtensions
    {
        /// <summary>
        /// Chain specifications with an and operation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s1">The first specifcation.</param>
        /// <param name="s2">The second specification.</param>
        /// <returns>A specification cooresponding to s1 &amp;&amp; s2</returns>
        public static ISpecification< T > And< T >( this ISpecification< T > s1, ISpecification< T > s2 )
        {
            return new AndSpecification< T >( s1, s2 );
        }

        /// <summary>
        /// Chain specifications with an or operation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s1">The first specifcation.</param>
        /// <param name="s2">The second specification.</param>
        /// <returns>A specification cooresponding to s1 || s2</returns>
        public static ISpecification< T > Or< T >( this ISpecification< T > s1, ISpecification< T > s2 )
        {
            return new OrSpecification< T >( s1, s2 );
        }

        /// <summary>
        /// Inverts the value of a specification
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s">The specification.</param>
        /// <returns>A specification cooresponding to !s</returns>
        public static ISpecification< T > Not< T >( this ISpecification< T > s )
        {
            return new NotSpecification< T >( s );
        }
    }
}