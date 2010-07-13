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
    // From http://stackoverflow.com/questions/2000311/generation-of-pseudo-random-constrained-values-of-uint64-and-decimal
    public static class RandomExtensions
    {
        private static int NextInt32( this Random rg )
        {
            unchecked
            {
                int firstBits = rg.Next( 0, 1 << 4 ) << 28;
                int lastBits = rg.Next( 0, 1 << 28 );
                return firstBits | lastBits;
            }
        }

        public static decimal NextDecimal( this Random rg )
        {
            bool sign = rg.Next( 2 ) == 1;
            return rg.NextDecimal( sign );
        }

        private static decimal NextDecimal( this Random rg, bool sign )
        {
            var scale = (byte)rg.Next( 29 );
            return new decimal( rg.NextInt32(),
                                rg.NextInt32(),
                                rg.NextInt32(),
                                sign,
                                scale );
        }

        private static decimal NextNonNegativeDecimal( this Random rg )
        {
            return rg.NextDecimal( false );
        }

        public static decimal NextDecimal( this Random rg, decimal maxValue )
        {
            return ( rg.NextNonNegativeDecimal() / Decimal.MaxValue ) * maxValue;
        }

        public static decimal NextDecimal( this Random rg, decimal minValue, decimal maxValue )
        {
            if ( minValue >= maxValue )
                throw new InvalidOperationException();
            decimal range = maxValue - minValue;
            return rg.NextDecimal( range ) + minValue;
        }

        public static long NextLong( this Random rg )
        {
            var bytes = new byte[sizeof(long)];
            rg.NextBytes( bytes );
            // strip out the sign bit
            bytes[ 7 ] = (byte)( bytes[ 7 ] & 0x7f );
            return BitConverter.ToInt64( bytes, 0 );
        }

        public static long NextLong( this Random rg, long maxValue )
        {
            return (long)( ( rg.NextLong() / (double)Int64.MaxValue ) * maxValue );
        }

        public static long NextLong( this Random rg, long minValue, long maxValue )
        {
            if ( minValue >= maxValue )
                throw new InvalidOperationException();
            long range = maxValue - minValue;
            return rg.NextLong( range ) + minValue;
        }
    }
}