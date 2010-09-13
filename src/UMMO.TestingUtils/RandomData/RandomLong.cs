using System;

namespace UMMO.TestingUtils.RandomData
{
    public class RandomLong : RandomNumericType<long>
    {
        public RandomLong( IRandom random ) : base( random ) {}

        #region Overrides of RandomNumericType<long>

        public override long Value
        {
            get { return Random.NextLong(); }
        }

        protected override long GetBetween( long min, long max )
        {
            return Random.NextLong( min, max );
        }

        #endregion
    }
}