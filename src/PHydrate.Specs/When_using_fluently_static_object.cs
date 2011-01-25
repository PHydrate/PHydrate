using Machine.Specifications;
using PHydrate.Core;

namespace PHydrate.Specs
{
    [ Subject( typeof(Fluently) ) ]
    public sealed class When_using_fluently_static_object
    {
        private Because Of = () => _returnedObject = Fluently.Configure;

        private It Should_return_object_of_type_fluent_configuration
            = () => _returnedObject.ShouldNotBeNull();

        private static FluentConfiguration _returnedObject;
    }
}