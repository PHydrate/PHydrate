namespace UMMO.TestingUtils.RandomData
{
    public class RandomDouble : RandomNumericType<double>
    {
        public RandomDouble( IRandom random ) : base( random ) {}

        public override double Value
        {
            get { return Random.NextDouble() * Random.Next(); }
        }

        protected override double GetBetween( double min, double max )
        {
            return ( Random.NextDouble() * ( max - min ) ) + min;
        }
    }
}