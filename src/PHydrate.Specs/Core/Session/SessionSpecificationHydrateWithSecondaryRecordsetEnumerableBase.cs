#region Copyright

// This file is part of PHydrate.
// 
// PHydrate is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// PHydrate is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with PHydrate.  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright 2010-2011, Stephen Michael Czetty

#endregion

using System.Collections.Generic;
using Machine.Specifications;
using PHydrate.Attributes;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace PHydrate.Specs.Core.Session
{
    public class SessionSpecificationHydrateWithSecondaryRecordsetEnumerableBase :
        SessionSpecificationHydrateWithSecondaryRecordsetBase
    {
        private Establish Context = () => {
                                        DataReaderMock.AddRecordSet( "AggregateKey" );
                                        DataReaderMock.AddRow( 1 );
                                        DataReaderMock.AddRow( 2 );
                                        DataReaderMock.AddRecordSet( "AggregateKey", "Key" );
                                        DataReaderMock.AddRow( 1, 1 );
                                        DataReaderMock.AddRow( 1, 2 );
                                        DataReaderMock.AddRow( 2, 3 );
                                        DataReaderMock.AddRow( 2, 4 );
                                        DataReaderMock.AddRow( 3, 5 );
                                        DataReaderMock.Playback();

                                        DatabaseService.Expect(
                                            x => x.ExecuteStoredProcedureReader( string.Empty, null ) ).Constraints(
                                                Is.Equal( "TestStoredProcedure" ), Is.NotNull() ).Return( DataReaderMock );
                                    };

        [ HydrateUsing( "TestStoredProcedure" ) ]
        protected class TestObjectSecondaryRecordsetIEnumerable
        {
            [ PrimaryKey ]
            public int AggregateKey { get; set; }

            [ Recordset( 1 ) ]
            public IEnumerable< TestObjectInternal > InnerObjects { get; set; }
        }

        [ HydrateUsing( "TestStoredProcedure" ) ]
        protected class TestObjectSecondaryRecordsetIList
        {
            [ PrimaryKey ]
            public int AggregateKey { get; set; }

            [ Recordset( 1 ) ]
            public IList< TestObjectInternal > InnerObjects { get; set; }
        }

        [ HydrateUsing( "TestStoredProcedure" ) ]
        protected class TestObjectSecondaryRecordsetIDictionary
        {
            [ PrimaryKey ]
            public int AggregateKey { get; set; }

            [ Recordset( 1 ) ]
            public IDictionary< int, TestObjectInternal > InnerObjects { get; set; }
        }
    }
}