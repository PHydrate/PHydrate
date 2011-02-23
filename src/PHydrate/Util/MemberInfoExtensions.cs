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

using System.Collections.Generic;
using System.Linq;
using PHydrate.Util.MemberInfoWrapper;

namespace PHydrate.Util
{
    /// <summary>
    ///   Extension methods on IMemberInfo
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        ///   Gets the lookup hash.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "internalRecordset">The internal recordset.</param>
        /// <param name = "obj">The obj.</param>
        /// <param name = "primaryKeyMembers">The primary key members.</param>
        /// <returns></returns>
        public static int GetLookupHash< T >( this IMemberInfo internalRecordset, object obj,
                                              IEnumerable< string > primaryKeyMembers ) where T : class
        {
            return typeof(T).GetObjectsHashCodeByFieldValues(
                internalRecordset.Type.GetMembersByName( primaryKeyMembers ).Select(
                    x => x.GetValue( obj ) ) );
        }
    }
}