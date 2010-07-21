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

namespace UMMO.TestingUtils.RandomData
{
    public class ExtendedRandom : Random, IRandom
    {
        #region IRandom Members

        public decimal NextDecimal()
        {
            return RandomExtensions.NextDecimal(this);
        }

        public decimal NextDecimal( decimal max )
        {
            return RandomExtensions.NextDecimal(this, max);
        }

        public decimal NextDecimal( decimal min, decimal max )
        {
            return RandomExtensions.NextDecimal(this, min, max);
        }

        public long NextLong()
        {
            return RandomExtensions.NextLong(this);
        }

        public long NextLong( long max )
        {
            return RandomExtensions.NextLong(this, max);
        }

        public long NextLong( long min, long max )
        {
            return RandomExtensions.NextLong(this, min, max);
        }

        #endregion
    }
}