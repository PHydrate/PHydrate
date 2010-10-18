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

        public override double Value
        {
            get { return Random.NextDouble() * Random.Next(); }
        }

        protected override double GetBetween( double minValue, double maxValue )
        {
            return ( Random.NextDouble() * ( maxValue - minValue ) ) + minValue;
        }
    }
}