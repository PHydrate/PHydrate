﻿#region Copyright

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

using Machine.Specifications;
using Machine.Specifications.Annotations;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    public abstract class DataReaderMockSpecsWithRecordSetDefined<T> : DataReaderMockSpecsBase
    {
        protected static string ColumnName;

        [ UsedImplicitly ]
        private Establish Context = () =>
                                        {
                                            ColumnName = A.Random.String.Resembling.A.Noun;
                                            MockUnderTest.AddRecordSet( ColumnName );
                                        };

        [UsedImplicitly]
        private Because Of = () => SetupTestRecord(ExpectedValue);

        protected static T ExpectedValue;

        private static void SetupTestRecord( T value )
        {
            MockUnderTest.AddRow( value ).Playback();
            MockUnderTest.Read();
        }
    }
}