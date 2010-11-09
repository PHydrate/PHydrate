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
// 

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
        /// <returns>A list of data parameters parsed from the expression</returns>
        public static IDictionary< string, Object > GetDataParameters< T >( this Expression< Func< T, bool > > expression )
        {
            var dataParameters = new Dictionary< string, Object >();

            // Go through the expression using tail recursion
            GetDataParametersRecursive( expression.Body, dataParameters );
            return dataParameters;
        }

        private static void GetDataParametersRecursive( Expression expression, IDictionary< string, object > dataParameters )
        {
            var operation = expression as BinaryExpression;
            if (operation != null)
            {
                switch (operation.NodeType)
                {
                    case ExpressionType.Equal:
                        string name = ( (MemberExpression)operation.Left ).Member.Name;
                        if ( !dataParameters.ContainsKey( name ) )
                            dataParameters.Add( name, GetValue( operation.Right ) );
                        break;

                    case ExpressionType.AndAlso:
                    case ExpressionType.And:
                        GetDataParametersRecursive( operation.Left, dataParameters );
                        GetDataParametersRecursive( operation.Right, dataParameters );
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private static object GetValue( Expression expression )
        {
            var targetMethodInfo = typeof(InvokeGeneric).GetMethod( "GetVariableValue" );
            var genericTargetCall = targetMethodInfo.MakeGenericMethod( expression.Type );
            return genericTargetCall.Invoke( new InvokeGeneric(), new[] {expression} );
        }

        #region InvokeGeneric - Helper class to get the value of the right-hand side of a lambda expression

        // This comes from http://stackoverflow.com/questions/238413/lambda-expression-tree-parsing
        private class InvokeGeneric
        {
            // ReSharper disable UnusedMember.Local
            public T GetVariableValue<T>(Expression expression)
            // ReSharper restore UnusedMember.Local
            {
                var accessorExpression = Expression.Lambda<Func<T>>(expression);
                var accessor = accessorExpression.Compile();
                return accessor();
            }
        }

        #endregion

    }
}