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

        /// <summary>
        /// Gets the random value.
        /// </summary>
        /// <value>The random value.</value>
        public override long Value
        {
            get { return Random.NextLong(); }
        }

        /// <summary>
        /// Return a random value of type long between the minimum and maximum.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns></returns>
        protected override long GetBetween( long minValue, long maxValue )
        {
            return Random.NextLong( minValue, maxValue );
        }

        #endregion
    }
}