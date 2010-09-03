#region Copyright

// This file is part of UMMO.
// 
// UMMO is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// UMMO is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with UMMO.  If not, see <http://www.gnu.org/licenses/>.
//  
// Copyright 2010, Stephen Michael Czetty

#endregion

using System.Collections.Generic;
using Machine.Specifications;
using UMMO.TestingUtils;

namespace UMMO.Extensions.Specs
{
    [Subject(typeof(IDictionaryExtensions))]
    public class When_calling_dictionary_try_get_value_extension_method
    {
        private static IDictionary< int, int > _dictionaryUnderTest;

        private Establish Context = () => _dictionaryUnderTest =
                                          new Dictionary< int, int > { { DictionaryKey, DictionaryValue } };

        private Because Of = () => _returnedValue = _dictionaryUnderTest.TryGetValue( DictionaryKey );

        private It Should_return_dictionary_value = () => _returnedValue.ShouldEqual( DictionaryValue );
                                        

        private static readonly int DictionaryKey = A.Random.Integer;
        private static readonly int DictionaryValue = A.Random.Integer;
        private static int _returnedValue;
    }
}