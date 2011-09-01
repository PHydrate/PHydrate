using Machine.Specifications;
using PHydrate.Core;

namespace PHydrate.Specs.Core.LroObjectCache
{
    [Subject(typeof(LroObjectCache<int>))]
    public sealed class When_identifier_type_is_guid_and_item_is_added_to_cache : LroObjectCacheSpecificationGuidBase
    {
        private Because Of = () => _retrievedObject = CacheToTest.GetFromCache<TestObjectToCache>(IdentifierValue);

        private It Should_be_added_to_cache
            = () => CacheToTest.IsInCache<TestObjectToCache>(IdentifierValue).ShouldBeTrue();

        private It Should_retrieve_item_from_cache
            = () => _retrievedObject.ShouldBeTheSameAs(TestObject);

        private static TestObjectToCache _retrievedObject;
    }
}