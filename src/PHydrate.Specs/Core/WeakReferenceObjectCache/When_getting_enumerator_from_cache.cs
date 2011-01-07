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

using System.Collections.Generic;
using Machine.Specifications;

namespace PHydrate.Specs.Core.WeakReferenceObjectCache
{
    [ Subject( typeof(PHydrate.Core.WeakReferenceObjectCache) ) ]
    [ SetupForEachSpecification ]
    public sealed class When_getting_enumerator_from_cache : WeakReferenceObjectCacheSpecificationBase
    {
        private static IEnumerator< object > _enumerator;
        private Establish Context = () => CacheUnderTest.Add( TestObject );

        private Because Of = () => _enumerator = GetEnumeratorAndMoveNext();

        private It Should_only_contain_one_item
            = () => _enumerator.MoveNext().ShouldBeFalse();

        private It Should_return_enumerator_containing_test_object
            = () => _enumerator.Current.ShouldBeTheSameAs( TestObject );

        private static IEnumerator< object > GetEnumeratorAndMoveNext()
        {
            IEnumerator< object > enumerator = CacheUnderTest.GetEnumerator();
            enumerator.MoveNext();
            return enumerator;
        }
    }
}