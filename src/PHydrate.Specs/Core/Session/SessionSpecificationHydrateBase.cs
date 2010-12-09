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
// Copyright 2010, Stephen Michael Czetty
// 

#endregion

using System.Collections.Generic;
using Machine.Specifications;
using PHydrate.Attributes;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace PHydrate.Specs.Core.Session
{
    public abstract class SessionSpecificationHydrateBase : SessionSpecificationBase
    {
        protected static IList< TestObject > RequestedObjects;

        #region Test Classes

        #region Nested type: TestObjectNoHydrator

        #endregion

        #region Nested type: TestObjectExplicitHydrator

        [HydrateUsing("TestStoredProcedure")]
        [ObjectHydrator(typeof(TestObjectHydrator))]
        protected class TestObjectExplicitHydrator
        {
            public int Key { get; set; }
        }

        #endregion

        #region Nested type: TestObjectHydrator

        private class TestObjectHydrator : IObjectHydrator<TestObjectExplicitHydrator>
        {
            #region Implementation of IObjectHydrator<TestObjectExplicitHydrator>

            public TestObjectExplicitHydrator Hydrate(IDictionary<string, object> columnValues)
            {
                return new TestObjectExplicitHydrator { Key = (int)columnValues["Key"] };
            }

            #endregion
        }

        #endregion

        #region Nested type: TestObjectExplicitHydrator

        [HydrateUsing("TestStoredProcedure")]
        [ObjectHydrator(typeof(TestObjectHydratorNoDefaultConstructor))]
        protected class TestObjectExplicitHydratorNoDefaultConstructor
        {
            public int Key { get; set; }
        }

        #endregion

        #region Nested type: TestObjectHydrator

        private class TestObjectHydratorNoDefaultConstructor : IObjectHydrator<TestObjectExplicitHydrator>
        {
            [CoverageExclude]
            public TestObjectHydratorNoDefaultConstructor([UsedImplicitly]int foo)
            {
            }

            #region Implementation of IObjectHydrator<TestObjectExplicitHydrator>

            [CoverageExclude]
            public TestObjectExplicitHydrator Hydrate(IDictionary<string, object> columnValues)
            {
                return new TestObjectExplicitHydrator { Key = (int)columnValues["Key"] };
            }

            #endregion
        }

        #endregion

        #endregion

        [ UsedImplicitly ]
        private Establish Context = () => {
                                        DataReaderMock.AddRecordSet( "Key" );
                                        DataReaderMock.AddRow( 1 );
                                        DataReaderMock.AddRow( 2 );
                                        DataReaderMock.Playback();

                                        DatabaseService.Expect( x => x.ExecuteStoredProcedureReader( "", null ) ).
                                            Constraints( Is.Equal( "TestStoredProcedure" ), Is.NotNull() ).Return(
                                                DataReaderMock );
                                    };
    }
}