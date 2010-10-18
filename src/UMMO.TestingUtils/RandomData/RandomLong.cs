using System;

namespace UMMO.TestingUtils.RandomData
{
    /// <summary>
    /// Fluent random long.
    /// </summary>
    public class RandomLong : RandomNumericType<long>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomLong"/> class.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public RandomLong( IRandom random ) : base( random ) {}

        #region Overrides of RandomNumericType<long>

        public override long Value
        {
            get { return Random.NextLong(); }
        }

        protected override long GetBetween( long minValue, long maxValue )
        {
            return Random.NextLong( minValue, maxValue );
        }

        #endregion
    }
}