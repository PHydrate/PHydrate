﻿#region Copyright

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

using System;
using Machine.Specifications;

namespace PHydrate.Specs.Core.WeakReferenceObjectCache
{
    [ Subject( typeof(PHydrate.Core.WeakReferenceObjectCache) ) ]
    public class When_copying_cache_to_null_array : WeakReferenceObjectCacheSpecificationBase
    {
        private static Exception _exception;
        private Establish Context = () => CacheUnderTest.Add( TestObject );

        // ReSharper disable AssignNullToNotNullAttribute
        private Because Of = () => _exception = Catch.Exception( () => CacheUnderTest.CopyTo( null, 0 ) );
        // ReSharper restore AssignNullToNotNullAttribute

        private It Should_throw_argument_null_exception
            = () => _exception.ShouldBeOfType< ArgumentNullException >();

        private It Should_throw_exception
            = () => _exception.ShouldNotBeNull();
    }
}