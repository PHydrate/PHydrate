using Machine.Specifications;
using PHydrate.Core;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.LroObjectCache
{
    [Subject(typeof(LroObjectCache<int>))]
    public sealed class When_identifier_type_is_integer_and_cache_is_cleaned_up : LroObjectCacheSpecificationIntBase
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