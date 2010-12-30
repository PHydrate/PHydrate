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

using Machine.Specifications;
using PHydrate.Util;

namespace PHydrate.Specs.Util.DictionaryExtensions
{
    [ Subject( typeof(PHydrate.Util.DictionaryExtensions) ) ]
    public class When_checking_for_key_without_case : DictionaryExtensionsSpecificationBase
    {
        private Because Of = () => Result = TestDictionary.ContainsKeyNoCase( RandomKey.ToLower(), out ActualName );

        private It Should_return_correct_name_in_output_parameter
            = () => ActualName.ShouldEqual( RandomKey );

        private It Should_return_true
            = () => Result.ShouldBeTrue();
    }
}