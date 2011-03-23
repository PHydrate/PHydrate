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
    /// <summary>
    /// Class for syntatic sugar returned by A.Random
    /// </summary>
    public class RandomDataGenerator
    {
        private readonly IRandom _random;
        private RandomInteger _randomInteger;
        private RandomString _randomString;
        private RandomDecimal _randomDecimal;
        private RandomDouble _randomDouble;
        private RandomLong _randomLong;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomDataGenerator"/> class.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        protected internal RandomDataGenerator(IRandom random)
        {
            _random = random;
        }

        /// <summary>
        /// A random fluent string.
        /// </summary>
        /// <value>The string.</value>
        public RandomString String
        {
            get { return _randomString ?? ( _randomString = new RandomString( _random ) ); }
        }

        /// <summary>
        /// A random fluent integer.
        /// </summary>
        /// <value>The integer.</value>
        public RandomInteger Integer
        {
            get { return _randomInteger ?? ( _randomInteger = new RandomInteger( _random ) ); }
        }

        /// <summary>
        /// A random boolean.
        /// </summary>
        /// <value>The boolean.</value>
        public bool Boolean
        {
            get { return _random.NextDouble() >= 0.5; }
        }

        /// <summary>
        /// A random byte.
        /// </summary>
        /// <value>The byte.</value>
        public byte Byte
        {
            get { return _random.NextBytes( 1 )[ 0 ]; }
        }

        /// <summary>
        /// A random character.
        /// </summary>
        /// <value>The character.</value>
        public char Character
        {
            get { return String.Password[ 0 ]; }
        }

        /// <summary>
        /// A random GUID.
        /// </summary>
        /// <value>The GUID.</value>
        public Guid Guid
        {
            get { return Guid.NewGuid(); }
        }

        /// <summary>
        /// A random short.
        /// </summary>
        /// <value>The short.</value>
        public short Short
        {
            get { return (short)_random.Next(); }
        }

        /// <summary>
        /// A random fluent long integer.
        /// </summary>
        /// <value>The long integer.</value>
        public RandomLong LongInteger
        {
            get { return _randomLong ?? ( _randomLong = new RandomLong( _random ) ); }
        }

        /// <summary>
        /// A random float.
        /// </summary>
        /// <value>The float.</value>
        public float Float
        {
            get { return (float)( _random.NextDouble() * _random.Next() ); }
        }

        /// <summary>
        /// A random fluent double.
        /// </summary>
        /// <value>The double.</value>
        public RandomDouble Double
        {
            get { return _randomDouble ?? ( _randomDouble = new RandomDouble( _random ) ); }
        }

        /// <summary>
        /// A random fluent decimal.
        /// </summary>
        /// <value>The decimal.</value>
        public RandomDecimal Decimal
        {
            get { return _randomDecimal ?? ( _randomDecimal = new RandomDecimal( _random ) ); }
        }

        /// <summary>
        /// A random date time.
        /// </summary>
        /// <value>The date time.</value>
        public DateTime DateTime
        {
            get { return new DateTime( _random.Next( 1970, 2100 ), _random.Next( 1, 12 ), _random.Next( 1, 28 ) ); }
        }
    }
    // ReSharper restore MemberCanBeMadeStatic.Global

    /// <summary>
    /// Utility class to simplify test code
    /// </summary>
    public static partial class A
    {
        private static RandomDataGenerator _randomDataGenerator;

        /// <summary>
        /// Fluent accessor for random data.
        /// </summary>
        /// <value>The random data accessor.</value>
        public static RandomDataGenerator Random
        {
            get { return _randomDataGenerator ?? (_randomDataGenerator = new RandomDataGenerator(new ExtendedRandom())); }
        }
    }
}