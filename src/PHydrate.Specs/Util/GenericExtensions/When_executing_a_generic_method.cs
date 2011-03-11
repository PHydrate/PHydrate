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

using System;
using Machine.Specifications;
using PHydrate.Util;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Util.GenericExtensions
{
    public class ExecuteGenericMethodBase {
        protected static TestClassWithGenericMethod ClassWithGenericMethod;
        private Establish Context = () => ClassWithGenericMethod = new TestClassWithGenericMethod();

        protected class TestClassWithGenericMethod
        {
            public T TestMethod< T >()
            {
                return default( T );
            }

            public T TestMethodWithArguments<T>(T arg)
            {
                return arg;
            }
        }
    }

    [ Subject( typeof(PHydrate.Util.GenericExtensions) ) ]
    public sealed class When_executing_a_generic_method : ExecuteGenericMethodBase
    {
        private Because Of =
            () =>
            _resultOfCall = ClassWithGenericMethod.ExecuteGenericMethod( x => x.TestMethod< object >(), typeof(int) );

        private It Should_return_integer
            = () => _resultOfCall.ShouldBeOfType< int >();

        private It Should_return_zero
            = () => _resultOfCall.ShouldEqual( 0 );

        private static object _resultOfCall;
    }

    [Subject(typeof(PHydrate.Util.GenericExtensions))]
    public sealed class When_calling_execute_generic_method_but_supplying_no_method_call : ExecuteGenericMethodBase
    {
        private Because Of =
            () =>
            _exception = Catch.Exception( () => ClassWithGenericMethod.ExecuteGenericMethod( x => 1, typeof(int) ) );

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_exception_of_type_phydrate_internal_exception
            = () => _exception.ShouldBeOfType< PHydrateInternalException >();

        private static Exception _exception;
    }

    [Subject(typeof(PHydrate.Util.GenericExtensions))]
    public sealed class When_executing_a_generic_method_with_arguments : ExecuteGenericMethodBase
    {
        private Establish Context = () => _randomInteger = A.Random.Integer;

        private Because Of =
            () =>
            _resultOfCall =
            ClassWithGenericMethod.ExecuteGenericMethod( x => x.TestMethodWithArguments( _randomInteger ), typeof(int) );

        private It Should_return_integer
            = () => _resultOfCall.ShouldBeOfType< int >();

        private It Should_return_random_value
            = () => _resultOfCall.ShouldEqual( _randomInteger );  

        private static int _randomInteger;
        private static object _resultOfCall;
    }
}