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
using PHydrate.Attributes;
using PHydrate.Core;

namespace PHydrate.Specs.Core.LroObjectCache
{
    public abstract class LroObjectCacheSpecificationBase<T>
    {
// ReSharper disable StaticFieldInGenericType
        protected static IObjectCache< T > CacheToTest;
        protected static TestObjectToCache TestObject;
        protected static T IdentifierValue;
// ReSharper restore StaticFieldInGenericType

        protected class TestObjectToCache
        {
            [PrimaryKey]
            public T Identifier { get; set; }
            public string StringValue { [UsedImplicitly]get; set; }
        }
        
        [UsedImplicitly]
        private Establish Context = () => CacheToTest = new LroObjectCache< T >(1);
    }
}