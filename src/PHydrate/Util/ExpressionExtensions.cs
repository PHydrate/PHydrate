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
using System.Diagnostics.CodeAnalysis;
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
        public static IDictionary< string, Object > GetDataParameters< T >(
            this Expression< Func< T, bool > > expression, string parameterPrefix )
        {
            var dataParameters = new Dictionary< string, Object >();

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
    }
}