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
using PHydrate.Util;

namespace PHydrate.Specs.Util.ExpressionExtensions
{
    [ Subject( typeof(PHydrate.Util.ExpressionExtensions) ) ]
    public sealed class When_getting_data_parameters_from_complex_expression_containing_or :
        ExpressionExtenionsSpecificationBase
    {
        private static Exception _exception;
        private Establish Context = () => ExpressionToTest = ( TestClass x ) => x.TestKey1 == 0 || x.TestKey2 == 1;

        private Because Of = () => _exception = Catch.Exception( () => ExpressionToTest.GetDataParameters( "@" ) );

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_throw_not_implemented_exception
            = () => _exception.ShouldBeOfType< NotImplementedException >();
    }
}