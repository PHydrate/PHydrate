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
    public class RandomInteger : RandomNumericType<int>
    {
        public RandomInteger( Random random ) : base( random ) {}

        public override int Value
        {
            get { return Random.Next(); }
        }

        public override int Between( int min, int max )
        {
            return Random.Next( min, max );
        }
    }
}