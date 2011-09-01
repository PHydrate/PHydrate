using System;
using Machine.Specifications;
using PHydrate.Core;
using UMMO.TestingUtils;

namespace PHydrate.Specs.Core.LroObjectCache
{
    [Subject(typeof(LroObjectCache<int>))]
    public sealed class When_identifier_type_is_integer_and_item_is_not_in_cache : LroObjectCacheSpecificationIntBase
    {
        private Establish Context = () => {
                                        _nonKeyInteger = IdentifierValue;
                                        while (_nonKeyInteger == IdentifierValue)
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