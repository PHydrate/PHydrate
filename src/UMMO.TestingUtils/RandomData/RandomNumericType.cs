using System;
using System.Reflection;

namespace UMMO.TestingUtils.RandomData
{
    public abstract class RandomNumericType<T> where T : struct
    {
        protected readonly Random Random;
        private readonly T _minValue;
        private readonly T _maxValue;

        protected internal RandomNumericType(Random random)
        {
            Random = random;
            var minValueField = typeof(T).GetField( "MinValue", BindingFlags.Static | BindingFlags.Public );
            var maxValueField = typeof(T).GetField( "MaxValue", BindingFlags.Static | BindingFlags.Public );

            if ( minValueField == null || maxValueField == null )
            {
                // The type is not compatible with this class.
                throw new RandomDataException( String.Format( "Could not create random data for type {0}",
                                                                       typeof(T).FullName ) );
            }
            _minValue = (T)minValueField.GetValue( new T() );
            _maxValue = (T)maxValueField.GetValue( new T() );
        }

        public abstract T Value { get; }

        public abstract T Between( T min, T max );

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
    }
}