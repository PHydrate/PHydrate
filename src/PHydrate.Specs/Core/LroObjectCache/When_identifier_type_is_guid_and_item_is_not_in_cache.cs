using System;
using Machine.Specifications;
using PHydrate.Core;

namespace PHydrate.Specs.Core.LroObjectCache
{
    [Subject(typeof(LroObjectCache<int>))]
    public sealed class When_identifier_type_is_guid_and_item_is_not_in_cache : LroObjectCacheSpecificationGuidBase
    {
        private Establish Context = () =>
                                    {
                                        _nonKeyGuid = IdentifierValue;
                                        while (_nonKeyGuid == IdentifierValue)
                                            _nonKeyGuid = Guid.NewGuid();
                                    };

        private Because Of =
            () => _exception = Catch.Exception(() => CacheToTest.GetFromCache<TestObjectToCache>(_nonKeyGuid));

        private It Should_not_be_in_cache
            = () => CacheToTest.IsInCache<TestObjectToCache>(_nonKeyGuid).ShouldBeFalse();

        private It Should_throw_phydrate_internal_exception_when_retrieving
            = () => _exception.ShouldBeOfType<PHydrateInternalException>();

        private static Guid _nonKeyGuid;
        private static Exception _exception;
    }
}