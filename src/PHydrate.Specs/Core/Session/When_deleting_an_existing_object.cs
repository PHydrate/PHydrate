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
using Rhino.Mocks;

namespace PHydrate.Specs.Core.Session
{
    [ Subject( typeof(PHydrate.Core.Session) ) ]
    public sealed class When_deleting_an_existing_object : SessionSpecificationDeleteSucceedsBase
    {
        private static TestObject _objectUnderTest;
        private Establish Context = () => { _objectUnderTest = new TestObject { Key = ExpectedKey }; };

        private Because Of = () => SessionUnderTest.Delete( _objectUnderTest );

        private It Should_call_stored_procedure
            = () => DatabaseService.VerifyAllExpectations();
    }
}