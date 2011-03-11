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

using Machine.Specifications;
using PHydrate.Util;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Util.TypeExtensions
{
    [ Subject( typeof(PHydrate.Util.TypeExtensions) ) ]
    public sealed class When_executing_a_generic_method : TypeExtensionsSpecificationBase
    {
        private Establish Context = () => _randomLong = A.Random.LongInteger;

        private Because Of =
            () => {
                _returnedInteger =
                    typeof(long).ExecuteGenericMethod< TestClassWithMethod< int >, int >( x => x.TestMethodNoArgs() );
                _returnedLong =
                    typeof(long).ExecuteGenericMethod< TestClassWithMethod< long >, long >(
                        x => x.TestMethodWithArgs( _randomLong ) );
            };

        private It Should_return_an_integer_from_no_args_method
            = () => _returnedInteger.ShouldBeOfType( typeof(int) );

        private It Should_return_one_from_no_args_method
            = () => _returnedInteger.ShouldEqual( 1 );

        private It Should_return_long_from_args_method
            = () => _returnedLong.ShouldBeOfType< long >();

        private It Should_return_random_long_from_args_method
            = () => _returnedLong.ShouldEqual( _randomLong );

        private static object _returnedInteger;
        private static long _randomLong;
        private static object _returnedLong;
    }
}