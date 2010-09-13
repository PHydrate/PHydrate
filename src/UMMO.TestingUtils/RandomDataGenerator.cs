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
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils
{
    // ReSharper disable MemberCanBeMadeStatic.Global
    public class RandomDataGenerator
    {
        private readonly IRandom _random;
        private RandomInteger _randomInteger;
        private RandomString _randomString;
        private RandomDecimal _randomDecimal;
        private RandomDouble _randomDouble;
        private RandomLong _randomLong;

        protected internal RandomDataGenerator(IRandom random)
        {
            _random = random;
        }

        public RandomString String
        {
            get { return _randomString ?? ( _randomString = new RandomString( _random ) ); }
        }

        public RandomInteger Integer
        {
            get { return _randomInteger ?? ( _randomInteger = new RandomInteger( _random ) ); }
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

        public RandomLong LongInteger
        {
            get { return _randomLong ?? ( _randomLong = new RandomLong( _random ) ); }
        }

        public float Float
        {
            get { return (float)( _random.NextDouble() * _random.Next() ); }
        }

        public RandomDouble Double
        {
            get { return _randomDouble ?? ( _randomDouble = new RandomDouble( _random ) ); }
        }

        public RandomDecimal Decimal
        {
            get { return _randomDecimal ?? ( _randomDecimal = new RandomDecimal( _random ) ); }
        }

        public DateTime DateTime
        {
            get { return new DateTime( _random.Next( 1970, 2100 ), _random.Next( 1, 12 ), _random.Next( 1, 28 ) ); }
        }
    }
    // ReSharper restore MemberCanBeMadeStatic.Global

    public static partial class A
    {
        private static RandomDataGenerator _randomDataGenerator;

        public static RandomDataGenerator Random
        {
            get { return _randomDataGenerator ?? (_randomDataGenerator = new RandomDataGenerator(new ExtendedRandom())); }
        }
    }
}