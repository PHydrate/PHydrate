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
using System.Reflection;

namespace PHydrate.Util
{
    /// <summary>
    ///   Extensions on ConstructorInfo
    /// </summary>
    public static class ConstructorInfoExtensions
    {
        /// <summary>
        ///   Returns true if the method's parameters matches the parameters passed in.
        /// </summary>
        /// <param name = "method">The method.</param>
        /// <param name = "constructorParameters">The constructor parameters.</param>
        /// <returns></returns>
        public static bool MatchesParameters( this MethodBase method, IList< object > constructorParameters )
        {
            ParameterInfo[] parameterInfos = method.GetParameters();
            if ( parameterInfos.Length != constructorParameters.Count )
                return false;
            return
                !constructorParameters.Where(
                    ( t, i ) => !parameterInfos[ i ].ParameterType.IsAssignableFrom( t.GetType() ) ).Any();
        }
    }
}