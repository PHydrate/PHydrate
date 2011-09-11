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
using Machine.Specifications;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.LroObjectCache
{
    [ Subject( typeof(PHydrate.Core.LroObjectCache) ) ]
    public sealed class When_item_is_not_in_cache : LroObjectCacheSpecificationIntBase
    {
        private Establish Context = () => {
                                        _nonKeyInteger = IdentifierValue;
                                        while ( _nonKeyInteger == IdentifierValue )
                                            _nonKeyInteger = A.Random.Integer;
                                    };

        private Because Of =
            () => _exception = Catch.Exception( () => CacheToTest.GetFromCache< TestObjectToCache >( _nonKeyInteger ) );

        private It Should_not_be_in_cache
            = () => CacheToTest.IsInCache< TestObjectToCache >( _nonKeyInteger ).ShouldBeFalse();

        private It Should_throw_phydrate_internal_exception_when_retrieving
            = () => _exception.ShouldBeOfType< PHydrateInternalException >();

        private static int _nonKeyInteger;
        private static Exception _exception;
    }
}