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
// Copyright 2010, Stephen Michael Czetty
// 

#endregion

using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using UMMO.TestingUtils;
using UMMO.TestingUtils.RandomData;

namespace PHydrate.Tests.Integration.Simple
{
    [ Subject( typeof(TestDomain.Simple), "Integration" ) ]
    [ Tags("Integration") ]
    public class When_getting_simple_type : PHydrateIntegrationTestBase
    {
        private static IList< TestDomain.Simple > _simpleList;

        private Because Of =
            () => _simpleList = SessionFactory.GetSession().Get< TestDomain.Simple >( x => x.SimpleId == 1 ).ToList();

        private It Should_populate_integer_value_with_one
            = () => _simpleList[ 0 ].IntegerValue.ShouldEqual( 1 );

        private It Should_populate_simple_id_with_one
            = () => _simpleList[ 0 ].SimpleId.ShouldEqual( 1 );

        private It Should_populate_string_value_with_test
            = () => _simpleList[ 0 ].StringValue.ShouldEqual( "test" );

        private It Should_return_a_single_record
            = () => _simpleList.Count.ShouldEqual( 1 );
    }

    [ Subject( typeof(TestDomain.Simple), "Integration" ) ]
    [ Tags( "Integration" ) ]
    public class When_creating_simple_type : PHydrateIntegrationTestBase
    {
        private Establish Context = () => {
                                        _integerValue = A.Random.Integer;
                                        _stringValue = A.Random.String;
                                        _newSimple = new TestDomain.Simple {
                                                                               IntegerValue = _integerValue,
                                                                               StringValue = _stringValue
                                                                           };
                                    };

        private Because Of = () => SessionFactory.GetSession().Persist( _newSimple );

        private It Should_populate_instance_with_id
            = () => _newSimple.SimpleId.ShouldBeGreaterThan( 0 );       

        private static int _integerValue;
        private static string _stringValue;
        private static TestDomain.Simple _newSimple;
    }
}