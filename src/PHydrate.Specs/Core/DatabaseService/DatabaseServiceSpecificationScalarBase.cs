﻿#region Copyright

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

using Machine.Specifications;
using Machine.Specifications.Annotations;
using Rhino.Mocks;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.DatabaseService
{
    public abstract class DatabaseServiceSpecificationScalarBase : DatabaseServiceSpecificationBase
    {
        protected static int ExpectedInteger;

        [ UsedImplicitly ]
        private Establish Context = () => {
                                        ExpectedInteger = A.Random.Integer;
                                        DbCommand.Expect( x => x.ExecuteScalar() ).Return( ExpectedInteger );
                                    };
    }
}