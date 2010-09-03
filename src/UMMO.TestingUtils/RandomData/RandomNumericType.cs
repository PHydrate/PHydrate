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
    public abstract class RandomNumericType< T > where T : struct, IComparable< T >
    {
        protected readonly IRandom Random;
        private readonly T _maxValue;
        private readonly T _minValue;

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

        public abstract T Value { get; }

        public T Between( T min, T max )
        {
            if ( min.CompareTo( max ) > 0 )
                throw new ArgumentException( "min must be less than or equal to max" );

            return GetBetween( min, max );
        }

        public T GreaterThan( T min )
        {
            return Between( min, _maxValue );
        }

        public T LessThan( T max )
        {
            return Between( _minValue, max );
        }

        public static implicit operator T( RandomNumericType< T > randomNumeric )
        {
            return randomNumeric.Value;
        }

        protected abstract T GetBetween( T min, T max );
    }
}