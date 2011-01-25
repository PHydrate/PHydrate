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

#endregion

using System;
using Machine.Specifications;
using UMMO.TestingUtils;

namespace PHydrate.Tests.Integration.Simple
{
    [ Subject( typeof(TestDomain.Simple), "Integration" ) ]
    [ Tags( "Integration" ) ]
    public sealed class When_deleting_simple_type_from_database : PHydrateIntegrationTestBase
    {
        private static long _integerValue;
        private static string _stringValue;
        private static TestDomain.Simple _newSimple;
        private static Exception _exception;
        private static long _simpleId;

        private Establish Context = () => {
                                        _integerValue = A.Random.Integer;
                                        _stringValue = A.Random.String;
                                        _newSimple = new TestDomain.Simple
                                                     { IntegerValue = _integerValue, StringValue = _stringValue };
                                        SessionFactory.GetSession().Persist( _newSimple );
                                        _simpleId = _newSimple.SimpleId;
                                    };

        private Because Of =
            () => _exception = Catch.Exception( () => SessionFactory.GetSession().Delete( _newSimple ) );

        private It Should_delete_object_from_database
            = () => SessionFactory.GetSession().Get< TestDomain.Simple >( x => x.SimpleId == _simpleId ).ShouldBeEmpty();

        private It Should_not_throw_an_exception
            = () => _exception.ShouldBeNull();
    }
}