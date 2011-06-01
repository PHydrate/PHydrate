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
using System.Collections.Generic;
#if NET40
using System.Collections.Concurrent;
#endif
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PHydrate.Specifications
{
    internal abstract class DbSpecificationCombinationBase< T > : DbSpecification< T > {
        protected readonly IDictionary< string, ParameterExpression > Parameters 
#if NET40
            = new ConcurrentDictionary< string, ParameterExpression >();
#else
            = new Dictionary<string, ParameterExpression>();
#endif

        protected Expression RebuildExpression( Expression expression )
        {  
            switch (expression.GetType().Name)
            {
                case "LogicalBinaryExpression":
                case "BinaryExpression":
                    var binaryExpression = ( (BinaryExpression)expression );
                    return Expression.MakeBinary( binaryExpression.NodeType,
                                                  RebuildExpression( binaryExpression.Left ),
                                                  RebuildExpression( binaryExpression.Right ) );


                case "ConditionalExpression":
                    var conditionalExpression = ( (ConditionalExpression)expression );
                    return Expression.Condition( conditionalExpression.Test,
                                                 RebuildExpression( conditionalExpression.IfTrue ),
                                                 RebuildExpression( conditionalExpression.IfFalse ) );


                case "ConstantExpression":
                    var constantExpression = ( (ConstantExpression)expression );
                    return Expression.Constant( constantExpression.Value, constantExpression.Type );


                case "MemberExpression":
                case "PropertyExpression":
                case "FieldExpression":
                    var memberExpression = ( (MemberExpression)expression );
                    var mi = memberExpression.Member as PropertyInfo;
                    if (mi != null)
                        return Expression.Property( RebuildExpression( memberExpression.Expression ), mi );
                    var fi = memberExpression.Member as FieldInfo;
                    if (fi != null)
                        return Expression.Field( RebuildExpression( memberExpression.Expression ), fi );
                    throw new PHydrateInternalException(
                        "Error parsing expression.  Found MemberExpression who's member is neither a property nor a field!" );


                case "MemberInitExpression":
                    var memberInitExpression = ( (MemberInitExpression)expression );
                    return
                        Expression.MemberInit(
                            (NewExpression)RebuildExpression( memberInitExpression.NewExpression ),
                            memberInitExpression.Bindings );


                case "MethodCallExpression":
                    var methodCallExpression = ( (MethodCallExpression)expression );
                    return Expression.Call( RebuildExpression( methodCallExpression.Object ),
                                            methodCallExpression.Method,
                                            methodCallExpression.Arguments.Select( RebuildExpression ) );

                case "NewArrayExpression":
                    var newArrayExpression = ( (NewArrayExpression)expression );
                    return newArrayExpression.NodeType == ExpressionType.NewArrayBounds
                               ? Expression.NewArrayBounds( newArrayExpression.Type,
                                                            newArrayExpression.Expressions.Select( RebuildExpression ) )
                               : Expression.NewArrayInit( newArrayExpression.Type,
                                                          newArrayExpression.Expressions.Select( RebuildExpression ) );

                case "NewExpression":
                    var newExpression = ( (NewExpression)expression );
                    return Expression.New( newExpression.Constructor,
                                           newExpression.Arguments.Select( RebuildExpression ) );      


                case "ParameterExpression":
                case "TypedParameterExpression":
                    // This is the whole reason for all this...
                    var parameterExpression = ( (ParameterExpression)expression );
                    if (!Parameters.ContainsKey(parameterExpression.Name))
                        Parameters[ parameterExpression.Name ] = Expression.Parameter( parameterExpression.Type,
                                                                                       parameterExpression.Name );
                    return Parameters[ parameterExpression.Name ];
                        

                    //case "TypeBinaryExpression":
                case "UnaryExpression":
                    var unaryExpression = ( (UnaryExpression)expression );
                    return Expression.MakeUnary( unaryExpression.NodeType,
                                                 RebuildExpression( unaryExpression.Operand ),
                                                 unaryExpression.Type, unaryExpression.Method );  
                    

                default:
                    throw new PHydrateInternalException( "Unexpected expression type encountered: {0}",
                                                         expression.GetType().Name );
            }
        }
    }

    internal class CombinedDbSpecification< T > : DbSpecificationCombinationBase< T >
    {
        private readonly Expression< Func< T, bool > > _criteria;

        public CombinedDbSpecification( DbSpecification< T > spec1, DbSpecification< T > spec2,
                                        ExpressionType expressionType )
        {
            _criteria = CombineExpressions( expressionType, spec1, spec2 );
        }

        public override Expression< Func< T, bool > > Criteria
        {
            get { return _criteria; }
        }

        protected Expression< Func< T, bool > > CombineExpressions( ExpressionType expressionType, DbSpecification<T> spec1, DbSpecification<T> spec2 )
        {
            var combinedExpression = Expression.MakeBinary( expressionType, spec1.Criteria.Body, spec2.Criteria.Body  );
            Parameters.Clear();
            return Expression.Lambda< Func< T, bool > >( RebuildExpression(combinedExpression ), Parameters.Values );
        }
    }
}