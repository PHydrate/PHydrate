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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PHydrate.Util
{
    /// <summary>
    ///   Extension methods on IMemberInfo
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        ///   Gets the lookup hash of a type, based on given values for the members
        /// </summary>
        /// <typeparam name = "T">They type to hash</typeparam>
        /// <param name = "internalRecordset">The internal recordset.</param>
        /// <param name = "obj">The obj.</param>
        /// <param name = "primaryKeyMembers">The primary key members.</param>
        /// <returns></returns>
        [ SuppressMessage( "Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter" ) ]
        [ SuppressMessage( "Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj" ) ]
        public static int GetLookupHash< T >( this IMemberInfo internalRecordset, object obj,
                                              params string[] primaryKeyMembers ) where T : class
        {
            return typeof(T).GetObjectsHashCodeByFieldValues(
                internalRecordset.Type.GetMembersByName( primaryKeyMembers ).Select(
                    x => x.GetValue( obj ) ) );
        }

        /// <summary>
        /// Gets a strongly-typed IEnumerable or IList
        /// </summary>
        /// <param name="internalRecordset">The internal recordset.</param>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="typeToCastTo">The type to cast to.</param>
        /// <returns></returns>
        public static object GetEnumerableOrList(this IMemberInfo internalRecordset, IEnumerable enumerable, Type typeToCastTo)
        {
            return internalRecordset.Type.IsAssignableFrom( typeof(IList< >).MakeGenericType( typeToCastTo ) )
                       ? enumerable.ToList( typeToCastTo )
                       : enumerable.Cast( typeToCastTo );
        }
    }
}