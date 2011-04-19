using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Rhino.Mocks;

namespace PHydrate.Specs.Core.Session
{
    [Subject(typeof(PHydrate.Core.Session))]
    public sealed class When_getting_an_object_containing_a_secondary_recordset_that_is_ienumerable_of_t :
        SessionSpecificationHydrateWithSecondaryRecordsetEnumerableBase
    {
        private Because Of =
            () =>
            _requestedObjects =
            SessionUnderTest.Get<TestObjectSecondaryRecordsetIEnumerable>(x => x.AggregateKey == 1).ToList();

        private It Should_call_stored_procedure
            = () => DatabaseService.VerifyAllExpectations();

        private It Should_call_stored_procedure_with_parameter_named_aggregate_key
            = () => AssertDatabaseServiceParameter("@AggregateKey", 1, x => x.ExecuteStoredProcedureReader(string.Empty, null));

        private It Should_include_correct_internal_records
            = () => _requestedObjects[0].InnerObjects.Count().ShouldEqual(2);

        private It Should_include_internal_record
            = () => _requestedObjects[0].InnerObjects.ShouldNotBeNull();

        private It Should_not_be_null
            = () => _requestedObjects.ShouldNotBeNull();

        private It Should_return_correct_record
            = () => _requestedObjects[0].AggregateKey.ShouldEqual(1);

        private static IList<TestObjectSecondaryRecordsetIEnumerable> _requestedObjects;
    }
}