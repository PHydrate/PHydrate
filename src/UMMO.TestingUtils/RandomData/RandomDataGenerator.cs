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
    // ReSharper disable MemberCanBeMadeStatic.Global
    public class RandomDataGenerator
    {
        private readonly Random _random;
        private readonly RandomInteger _randomInteger;
        private readonly RandomString _randomString;

        protected internal RandomDataGenerator(Random random)
        {
            _random = random;
            _randomInteger = new RandomInteger( _random );
            _randomString = new RandomString( _random );
        }

        public RandomString String
        {
            get { return _randomString; }
        }

        public RandomInteger Integer
        {
            get { return _randomInteger; }
        }

        public bool Boolean
        {
            get { return _random.NextDouble() >= 0.5; }
        }

        public byte Byte
        {
            get
            {
                var bytes = new byte[1];
                _random.NextBytes( bytes );
                return bytes[ 0 ];
            }
        }

        public char Character
        {
            get { return String.Password[ 0 ]; }
        }

        public Guid Guid
        {
            get { return Guid.NewGuid(); }
        }

        public short Short
        {
            get { return (short)_random.Next(); }
        }

        public long LongInteger
        {
            get { return _random.Next(); }
        }

        public float Float
        {
            get { return (float)( _random.NextDouble() * _random.Next() ); }
        }

        public double Double
        {
            get { return ( _random.NextDouble() * _random.Next() ); }
        }

        public decimal Decimal
        {
            get { return (decimal)( _random.NextDouble() * _random.Next() ); }
        }

        public DateTime DateTime
        {
            get { return new DateTime( _random.Next( 1970, 2100 ), _random.Next( 1, 12 ), _random.Next( 1, 28 ) ); }
        }
    }
    // ReSharper restore MemberCanBeMadeStatic.Global
}