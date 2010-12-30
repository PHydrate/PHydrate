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

namespace PHydrate.Specs.Core.WeakReferenceObjectCache
{
    [ Subject( typeof(PHydrate.Core.WeakReferenceObjectCache) ) ]
    public class When_removing_item_from_array : WeakReferenceObjectCacheSpecificationBase
    {
        private Establish Context = () => CacheUnderTest.Add( TestObject );

        private Because Of = () => CacheUnderTest.Remove( TestObject );

        private It Should_contain_zero_records
            = () => CacheUnderTest.Count.ShouldEqual( 0 );

        private It Should_not_contain_the_test_object
            = () => CacheUnderTest.Contains( TestObject ).ShouldBeFalse();
    }
}