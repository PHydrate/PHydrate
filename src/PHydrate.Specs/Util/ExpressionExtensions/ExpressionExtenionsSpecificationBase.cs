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

using System;
using System.Linq.Expressions;
using Machine.Specifications;
using Machine.Specifications.Annotations;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Util.ExpressionExtensions
{
    public abstract class ExpressionExtenionsSpecificationBase
    {
        #region Test Class

        public class TestClass
        {
            public int TestKey1 { get; set; }
            public int TestKey2 { get; set; }
        }

        #endregion

        protected static Expression< Func< TestClass, bool > > ExpressionToTest;
        protected static int RandomInteger1;
        protected static int RandomInteger2;

        [ UsedImplicitly ]
        private Establish Context = () => {
                                        RandomInteger1 = A.Random.Integer;
                                        RandomInteger2 = A.Random.Integer;
                                    };
    }
}