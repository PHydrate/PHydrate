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

namespace PHydrate.Specs.Core.WeakReferenceObjectCache
{
    [ Subject( typeof(PHydrate.Core.WeakReferenceObjectCache) ) ]
    public sealed class When_copying_cache_to_array_with_not_enough_room_in_array :
        WeakReferenceObjectCacheSpecificationBase
    {
        private static object[] _testArray = new object[1];
        private static Exception _exception;

        private Establish Context = () => {
                                        CacheUnderTest.Add( TestObject );
                                        CacheUnderTest.Add( new { x = 1 } );
                                    };

        private Because Of = () => _exception = Catch.Exception( () => CacheUnderTest.CopyTo( _testArray, 1 ) );

        private It Should_throw_argument_exception
            = () => _exception.ShouldBeOfType< ArgumentException >();

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();
    }
}