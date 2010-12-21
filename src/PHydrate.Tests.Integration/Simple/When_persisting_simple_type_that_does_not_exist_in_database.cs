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

using Machine.Specifications;
using UMMO.TestingUtils;

namespace PHydrate.Tests.Integration.Simple
{
    [ Subject( typeof(TestDomain.Simple), "Integration" ) ]
    [ Tags( "Integration" ) ]
    public class When_persisting_simple_type_that_does_not_exist_in_database : PHydrateIntegrationTestBase
    {
        private static int _integerValue;
        private static string _stringValue;
        private static TestDomain.Simple _newSimple;

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
    }
}