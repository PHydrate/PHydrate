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
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.LroObjectCache
{
    [ Subject( typeof(PHydrate.Core.LroObjectCache) ) ]
    public sealed class When_cache_is_cleaned_up : LroObjectCacheSpecificationIntBase
    {
        private Establish Context =
            () => {
                _newIdentifier = A.Random.Integer;
                CacheToTest.AddToCache( _newIdentifier, new TestObjectToCache { Identifier = _newIdentifier } );
            };

        private Because Of = () => CacheToTest.Cleanup();

        private It Should_remove_older_cached_object
            = () => CacheToTest.IsInCache< TestObjectToCache >( IdentifierValue ).ShouldBeFalse();

        private It Should_retain_newer_cached_object
            = () => CacheToTest.IsInCache< TestObjectToCache >( _newIdentifier ).ShouldBeTrue();

        private static int _newIdentifier;
    }
}