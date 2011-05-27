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

namespace PHydrate.Specs.Core.WeakReferenceObjectCache
{
    [ Subject( typeof(PHydrate.Core.WeakReferenceObjectCache) ) ]
    public sealed class When_adding_an_item_to_the_cache : WeakReferenceObjectCacheSpecificationBase
    {
        private Because Of = () => CacheUnderTest.Add( TestObject );

        private It Should_not_be_read_only
            = () => CacheUnderTest.IsReadOnly.ShouldBeFalse();

        private It Should_return_count_of_one
            = () => CacheUnderTest.Count.ShouldEqual( 1 );

        private It Should_specify_that_the_cache_contains_the_object
            = () => CacheUnderTest.Contains( TestObject ).ShouldBeTrue();

        private It Should_store_object
            = () => CacheUnderTest.ShouldContain( TestObject );
    }
}