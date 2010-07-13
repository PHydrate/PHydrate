using System;

namespace UMMO.TestingUtils.RandomData
{
    public class RandomDecimal : RandomNumericType<decimal>
    {
        public RandomDecimal( IRandom random ) : base( random ) {}

        public override decimal Value
        {
            get { return Random.NextDecimal(); }
        }

        protected override decimal GetBetween( decimal min, decimal max )
        {
            return Random.NextDecimal(min, max);
        }
    }
}