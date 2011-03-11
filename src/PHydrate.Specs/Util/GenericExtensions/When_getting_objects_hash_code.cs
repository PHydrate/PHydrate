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

using Machine.Specifications;
using PHydrate.Util;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Util.GenericExtensions
{
    [ Subject( typeof(PHydrate.Util.GenericExtensions) ) ]
    public sealed class When_getting_objects_hash_code
    {
        private Establish Context = () => _randomString = A.Random.String;

        private Because Of = () => _hashCode = _randomString.GetObjectsHashCodeByPrimaryKeys();

        private It Should_equal_calculated_hashcode
            =
            () =>
            _hashCode.ShouldEqual( ( 73 * 137 + typeof(string).GetHashCode() ) * 137 + _randomString.GetHashCode() );

        private static string _randomString;
        private static int _hashCode;
    }
}