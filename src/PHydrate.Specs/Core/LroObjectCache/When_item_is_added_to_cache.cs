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

namespace PHydrate.Specs.Core.LroObjectCache
{
    [ Subject( typeof(PHydrate.Core.LroObjectCache) ) ]
    public sealed class When_item_is_added_to_cache : LroObjectCacheSpecificationIntBase
    {
        private Because Of = () => _retrievedObject = CacheToTest.GetFromCache< TestObjectToCache >( IdentifierValue );

        private It Should_be_added_to_cache
            = () => CacheToTest.IsInCache< TestObjectToCache >( IdentifierValue ).ShouldBeTrue();

        private It Should_retrieve_item_from_cache
            = () => _retrievedObject.ShouldBeTheSameAs( TestObject );

        private static TestObjectToCache _retrievedObject;
    }
}