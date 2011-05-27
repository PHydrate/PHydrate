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

using PHydrate.Attributes;

namespace PHydrate.Specs.Util.TypeExtensions
{
    public abstract class TypeExtensionsSpecificationBase
    {
        #region Nested type: TestClass

        [ CreateUsing( "" ) ]
        protected class TestClass {}

        #endregion

        #region Nested type: TestClassWithNoDefaultConstructor

        protected class TestClassWithNoDefaultConstructor
        {
            [ CoverageExclude ]
            public TestClassWithNoDefaultConstructor( int blank ) {}
        }

        #endregion

        [ UsedImplicitly ]
        protected class TestClassWithMethod< T >
        {
            public int TestMethodNoArgs()
            {
                return 1;
            }

            public T TestMethodWithArgs( T longValue )
            {
                return longValue;
            }
        }
    }
}