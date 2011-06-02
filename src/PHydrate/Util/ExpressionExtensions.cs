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
#if NET40
using System.Collections.Concurrent;
#endif
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PHydrate.Attributes;

namespace PHydrate.Util
{
    /// <summary>
    /// Extension methods for Expressions
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Gets the data parameters from an expression returning a boolean.
        /// </summary>
        /// <typeparam name="T">The type being acted on</typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="parameterPrefix">The prefix to add to each parameter name</param>
        /// <returns>A list of data parameters parsed from the expression</returns>
        // TODO: Return type should be changed to IEnumerable<KeyValuePair<string, Object>>
        [ NotNull ]
        [ SuppressMessage( "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters" ) ]
        [ SuppressMessage( "Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures" ) ]
        public static IDictionary< string, object > GetDataParameters< T >(
            this Expression< Func< T, bool > > expression, string parameterPrefix )
        {
            var dataParameters = new Dictionary< string, object >();

            // Go through the expression using tail recursion
            if ( expression != null )
                GetDataParametersRecursive( expression.Body, dataParameters, parameterPrefix );
            return dataParameters;
        }

        private static void GetDataParametersRecursive( Expression expression,
                                                        IDictionary< string, object > dataParameters,
                                                        string parameterPrefix )
        {
            var operation = expression as BinaryExpression;
            if ( operation == null )
                return;

            switch ( operation.NodeType )
            {
                case ExpressionType.Equal:
                    string name = parameterPrefix + ( (MemberExpression)operation.Left ).Member.Name;
                    if ( !dataParameters.ContainsKey( name ) )
                        dataParameters.Add( name, operation.Right.GetValue() );
                    break;

                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    GetDataParametersRecursive( operation.Left, dataParameters, parameterPrefix );
                    GetDataParametersRecursive( operation.Right, dataParameters, parameterPrefix );
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the value from an expression (invokes it).
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static object GetValue( this Expression expression )
        {
            var targetMethodInfo = typeof(InvokeGeneric).GetMethod( "GetVariableValue",
                                                                    BindingFlags.Static | BindingFlags.Public );
            var genericTargetCall = targetMethodInfo.MakeGenericMethod( expression.Type );
            return genericTargetCall.Invoke( null, new[] { expression } );
        }

        #region InvokeGeneric - Helper class to get the value of the right-hand side of a lambda expression

        // This comes from http://stackoverflow.com/questions/238413/lambda-expression-tree-parsing
        private static class InvokeGeneric
        {
            [ UsedImplicitly ]
            public static T GetVariableValue< T >( Expression expression )
            {
                var accessorExpression = Expression.Lambda< Func< T > >( expression );
                var accessor = accessorExpression.Compile();
                return accessor();
            }
        }

        #endregion

        /// <summary>
        /// Builds the lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static Expression< Func< T, bool > > RebuildLambdaExpression< T >(this Expression expression)
        {
            var parameters =
#if NET40
                new ConcurrentDictionary< string, ParameterExpression >();
#else
                new Dictionary<string, ParameterExpression>();
#endif

            return Expression.Lambda< Func< T, bool > >( RebuildExpression( expression, parameters ), parameters.Values );
        }

        private static Expression RebuildExpression( Expression expression, IDictionary< string, ParameterExpression > parameters )
        {  
            switch (expression.GetType().Name)
            {
                case "LogicalBinaryExpression":
                case "BinaryExpression":
                    var binaryExpression = ( (BinaryExpression)expression );
                    return Expression.MakeBinary( binaryExpression.NodeType,
                                                  RebuildExpression( binaryExpression.Left, parameters ),
                                                  RebuildExpression( binaryExpression.Right, parameters ) );


                case "ConditionalExpression":
                    var conditionalExpression = ( (ConditionalExpression)expression );
                    return Expression.Condition( conditionalExpression.Test,
                                                 RebuildExpression(conditionalExpression.IfTrue, parameters),
                                                 RebuildExpression(conditionalExpression.IfFalse, parameters));


                case "ConstantExpression":
                    var constantExpression = ( (ConstantExpression)expression );
                    return Expression.Constant( constantExpression.Value, constantExpression.Type );


                case "MemberExpression":
                case "PropertyExpression":
                case "FieldExpression":
                    var memberExpression = ( (MemberExpression)expression );
                    var mi = memberExpression.Member as PropertyInfo;
                    if (mi != null)
                        return Expression.Property(RebuildExpression(memberExpression.Expression, parameters), mi);
                    var fi = memberExpression.Member as FieldInfo;
                    if (fi != null)
                        return Expression.Field(RebuildExpression(memberExpression.Expression, parameters), fi);
                    throw new PHydrateInternalException(
                        "Error parsing expression.  Found MemberExpression who's member is neither a property nor a field!" );


                case "MemberInitExpression":
                    var memberInitExpression = ( (MemberInitExpression)expression );
                    return
                        Expression.MemberInit(
                            (NewExpression)RebuildExpression(memberInitExpression.NewExpression, parameters),
                            memberInitExpression.Bindings );


                case "MethodCallExpression":
                    var methodCallExpression = ( (MethodCallExpression)expression );
                    return Expression.Call( RebuildExpression( methodCallExpression.Object, parameters ),
                                            methodCallExpression.Method,
                                            methodCallExpression.Arguments.Select( x => RebuildExpression(x, parameters) ) );

                case "NewArrayExpression":
                    var newArrayExpression = ( (NewArrayExpression)expression );
                    return newArrayExpression.NodeType == ExpressionType.NewArrayBounds
                               ? Expression.NewArrayBounds( newArrayExpression.Type,
                                                            newArrayExpression.Expressions.Select( x => RebuildExpression(x, parameters) ) )
                               : Expression.NewArrayInit( newArrayExpression.Type,
                                                          newArrayExpression.Expressions.Select( x => RebuildExpression(x, parameters) ) );

                case "NewExpression":
                    var newExpression = ( (NewExpression)expression );
                    return Expression.New( newExpression.Constructor,
                                           newExpression.Arguments.Select(x => RebuildExpression(x, parameters) ) );      


                case "ParameterExpression":
                case "TypedParameterExpression":
                    // This is the whole reason for all this...
                    var parameterExpression = ( (ParameterExpression)expression );
                    if (!parameters.ContainsKey(parameterExpression.Name))
                        parameters[ parameterExpression.Name ] = Expression.Parameter( parameterExpression.Type,
                                                                                       parameterExpression.Name );
                    return parameters[ parameterExpression.Name ];
                        

                    //case "TypeBinaryExpression":
                case "UnaryExpression":
                    var unaryExpression = ( (UnaryExpression)expression );
                    return Expression.MakeUnary( unaryExpression.NodeType,
                                                 RebuildExpression( unaryExpression.Operand, parameters ),
                                                 unaryExpression.Type, unaryExpression.Method );  
                    

                default:
                    throw new PHydrateInternalException( "Unexpected expression type encountered: {0}",
                                                         expression.GetType().Name );
            }
        }
    }
}