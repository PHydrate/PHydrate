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

namespace UMMO.TestingUtils
{
    public class RandomInteger
    {
        private readonly Random _random;

        protected internal RandomInteger( Random random )
        {
            _random = random;
        }

        public int Int
        {
            get { return _random.Next(); }
        }

        public int Between( int min, int max )
        {
            return _random.Next( min, max );
        }

        public int GreaterThan( int min )
        {
            return Between( min, int.MaxValue );
        }

        public int LessThan( int max )
        {
            return Between( int.MinValue, max );
        }

        public static implicit operator int( RandomInteger randomInteger )
        {
            return randomInteger.Int;
        }
    }
}