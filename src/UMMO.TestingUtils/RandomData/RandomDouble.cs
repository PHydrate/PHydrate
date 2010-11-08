namespace UMMO.TestingUtils.RandomData
{
    /// <summary>
    /// Fluent random double.
    /// </summary>
    public class RandomDouble : RandomNumericType<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomDouble"/> class.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public RandomDouble( IRandom random ) : base( random ) {}

        /// <summary>
        /// Gets the random value.
        /// </summary>
        /// <value>The random value.</value>
        public override double Value
        {
            get { return Random.NextDouble() * Random.Next(); }
        }

        /// <summary>
        /// Return a random value of type double between the minimum and maximum.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns></returns>
        protected override double GetBetween( double minValue, double maxValue )
        {
            return ( Random.NextDouble() * ( maxValue - minValue ) ) + minValue;
        }
    }
}