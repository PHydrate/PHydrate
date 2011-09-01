using System;
using Machine.Specifications;
using PHydrate.Attributes;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.LroObjectCache
{
    public abstract class LroObjectCacheSpecificationGuidBase : LroObjectCacheSpecificationBase<Guid>
    {
        [UsedImplicitly]
        private Establish Context = () =>
                                    {
                                        IdentifierValue = Guid.NewGuid();
                                        TestObject = new TestObjectToCache { Identifier = IdentifierValue, StringValue = A.Random.String };
                                        CacheToTest.AddToCache(IdentifierValue, TestObject);
                                    };
    }
}