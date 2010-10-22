using System;
using Machine.Specifications;
using PHydrate.Attributes;
using Rhino.Mocks;

namespace PHydrate.Specs.Attributes
{
    [Subject(typeof(ObjectHydratorAttribute))]
    public class When_instantiating_object_hydrator_attribute_specifying_an_implementation_of_i_object_hydrator
    {
        private Establish Context = () => {
                                        var mockObjectHydrator =
                                            MockRepository.GenerateStub< IObjectHydrator< object > >();
                                        _hydratorType = mockObjectHydrator.GetType();
                                    };

        private Because Of = () => _objectHydratorAttribute = new ObjectHydratorAttribute(_hydratorType);

        private It Should_store_type_in_property
            = () => _objectHydratorAttribute.HydratorType.ShouldEqual( _hydratorType );

        private static Type _hydratorType;
        private static ObjectHydratorAttribute _objectHydratorAttribute;
    }

    [Subject(typeof(ObjectHydratorAttribute))]
    public class When_instantiating_object_hydrator_attribute_specifying_a_class_that_does_not_implement_i_object_hydrator
    {
        private Because Of = () => _exception = Catch.Exception(() => new ObjectHydratorAttribute( typeof(Object) ));

        private It Should_throw_custom_exception
            = () => _exception.ShouldBeOfType( typeof(PHydrateException) );

        private static Exception _exception;
    }
}