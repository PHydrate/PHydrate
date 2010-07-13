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
    public class RandomWrapper : IRandom
    {
        private readonly Random _random = new Random();

        #region IRandom Members

        public int Next()
        {
            return _random.Next();
        }

        public int Next( int max )
        {
            return _random.Next( max );
        }

        public int Next( int min, int max )
        {
            return _random.Next( min, max );
        }

        public void NextBytes( byte[] buffer )
        {
            _random.NextBytes( buffer );
        }

        public double NextDouble()
        {
            return _random.NextDouble();
        }

        public decimal NextDecimal()
        {
            return _random.NextDecimal();
        }

        public decimal NextDecimal( decimal max )
        {
            return _random.NextDecimal( max );
        }

        public decimal NextDecimal( decimal min, decimal max )
        {
            return _random.NextDecimal( min, max );
        }

        public long NextLong()
        {
            return _random.NextLong();
        }

        public long NextLong( long max )
        {
            return _random.NextLong( max );
        }

        public long NextLong( long min, long max )
        {
            return _random.NextLong( min, max );
        }

        #endregion
    }
}