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

namespace UMMO.TestingUtils.RandomData
{
    /// <summary>
    /// Fluent random decimal.
    /// </summary>
    public class RandomDecimal : RandomNumericType<decimal>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomDecimal"/> class.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public RandomDecimal( IRandom random ) : base( random ) {}

        /// <summary>
        /// Gets the random value.
        /// </summary>
        /// <value>The random value.</value>
        public override decimal Value
        {
            get { return Random.NextDecimal(); }
        }

        /// <summary>
        /// Return a random value of type decimal between the minimum and maximum.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns></returns>
        protected override decimal GetBetween( decimal minValue, decimal maxValue )
        {
            return Random.NextDecimal(minValue, maxValue);
        }
    }
}