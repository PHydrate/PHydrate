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

using System.Collections.Generic;
using Machine.Specifications;
using Machine.Specifications.Annotations;

namespace UMMO.TestingUtils.Specs.DataReaderMock
{
    public abstract class DataReaderMockSpecsUsingObsoleteConstructor
    {
        protected static IList< KeyValuePair< string, object > > RecordSet;
        protected static TestingUtils.DataReaderMock MockUnderTest;
        private static string _columnOneName;
        private static int _columnOneValue;
        private static string _columnTwoName;
        private static string _columnTwoValue;

        [ UsedImplicitly ]
        private Establish Context = () =>
                                        {
                                            _columnOneName = A.Random.FirstName;
                                            _columnOneValue = A.Random.Integer;
                                            _columnTwoName = A.Random.LastName;
                                            _columnTwoValue = A.Random.Password;
                                            RecordSet = new List< KeyValuePair< string, object > >
                                                            {
                                                                new KeyValuePair< string, object >( _columnOneName,
                                                                                                    _columnOneValue ),
                                                                new KeyValuePair< string, object >( _columnTwoName,
                                                                                                    _columnTwoValue )
                                                            };
                                        };
    }
}