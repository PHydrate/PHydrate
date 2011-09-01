using Machine.Specifications;
using PHydrate.Attributes;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.LroObjectCache
{
    public abstract class LroObjectCacheSpecificationIntBase : LroObjectCacheSpecificationBase< int >
    {
        [ UsedImplicitly ]
        private Establish Context = () => {
                                        IdentifierValue = A.Random.Integer;
                                        TestObject = new TestObjectToCache
                                                     { Identifier = IdentifierValue, StringValue = A.Random.String };
                                        CacheToTest.AddToCache( IdentifierValue, TestObject );
                                    };
    }
}