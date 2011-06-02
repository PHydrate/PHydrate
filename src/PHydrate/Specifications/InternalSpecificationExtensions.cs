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
using System.Linq.Expressions;
using PHydrate.Util;

namespace PHydrate.Specifications
{
    internal static class InternalSpecificationExtensions
    {
        /// <summary>
        /// Combines two specifications using the specified boolean expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spec1">The spec1.</param>
        /// <param name="spec2">The spec2.</param>
        /// <param name="booleanExpressionType">Type of the expression.</param>
        /// <returns></returns>
        public static Expression< Func< T, bool > > CombineWith< T >( this DbSpecification< T > spec1,
                                                                      DbSpecification< T > spec2,
                                                                      ExpressionType booleanExpressionType )
        {
            var combinedExpression = Expression.MakeBinary( booleanExpressionType, spec1.Criteria.Body,
                                                            spec2.Criteria.Body );

            return combinedExpression.RebuildLambdaExpression< T >();
        }

        public static Expression< Func< T, bool > > Invert< T >( this DbSpecification< T > dbSpecification )
        {
            var invertedExpression = Expression.MakeUnary( ExpressionType.Not, dbSpecification.Criteria.Body,
                                                           typeof(bool) );

            return invertedExpression.RebuildLambdaExpression< T >();
        }
    }
}