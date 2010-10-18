#region Copyright

// This file is part of UMMO.
// 
// UMMO is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// UMMO is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with UMMO.  If not, see <http://www.gnu.org/licenses/>.
//  
// Copyright 2010, Stephen Michael Czetty

#endregion

using System;
using System.Reflection;

namespace UMMO.TestingUtils.RandomData
{
    /// <summary>
    /// Base class for fluent random classes
    /// </summary>
    /// <typeparam name="T">The type of random value being created</typeparam>
    public abstract class RandomNumericType< T > where T : struct, IComparable< T >
    {
        protected readonly IRandom Random;
        private readonly T _maxValue;
        private readonly T _minValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomNumericType&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        /// <exception cref="UMMO.TestingUtils.RandomDataException">
        ///     Thrown when <typeparamref name="T"/> does not define "MinValue" or "MaxValue" fields.
        /// </exception>
        protected RandomNumericType( IRandom random )
        {
            Random = random;
            var minValueField = typeof(T).GetField( "MinValue", BindingFlags.Static | BindingFlags.Public );
            var maxValueField = typeof(T).GetField( "MaxValue", BindingFlags.Static | BindingFlags.Public );

            if ( minValueField == null || maxValueField == null )
                // The type is not compatible with this class.
                throw new RandomDataException( String.Format( "Could not create random data for type {0}",
                                                              typeof(T).FullName ) );
            _minValue = (T)minValueField.GetValue( new T() );
            _maxValue = (T)maxValueField.GetValue( new T() );
        }

        // For test coverage
        [Obsolete("The parameterless constructor should only be used for test coverage")]
        internal RandomNumericType() {}

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public abstract T Value { get; }

        /// <summary>
        /// Return a value between <paramref name="minValue"/> and <paramref name="maxValue"/>.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>A random value of type <typeparamref name="T"/></returns>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="minValue"/> is greater than <paramref name="maxValue"/>.
        /// </exception>
        public T Between( T minValue, T maxValue )
        {
            if ( minValue.CompareTo( maxValue ) > 0 )
                throw new ArgumentException( "minValue must be less than or equal to maxValue" );

            return GetBetween( minValue, maxValue );
        }

        /// <summary>
        /// Return a value greater than <paramref name="minValue"/>.
        /// </summary>
        /// <param name="minValue">The min.</param>
        /// <returns>A random value of type <typeparamref name="T"/></returns>
        public T GreaterThan( T minValue )
        {
            return Between( minValue, _maxValue );
        }

        /// <summary>
        /// Return a value less than <paramref name="maxValue"/>
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>A random value of type <typeparamref name="T"/></returns>
        public T LessThan( T maxValue )
        {
            return Between( _minValue, maxValue );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UMMO.TestingUtils.RandomData.RandomNumericType&lt;T&gt;"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="randomNumeric">The random numeric.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator T( RandomNumericType< T > randomNumeric )
        {
            return randomNumeric.Value;
        }

        /// <summary>
        /// Return a random value of type <typeparamref name="T"/> between the minimum and maximum.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns></returns>
        protected abstract T GetBetween( T minValue, T maxValue );
    }
}