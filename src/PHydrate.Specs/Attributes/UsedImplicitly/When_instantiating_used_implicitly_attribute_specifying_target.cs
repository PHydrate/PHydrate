using Machine.Specifications;
using PHydrate.Attributes;

namespace PHydrate.Specs.Attributes.UsedImplicitly
{
    [Subject(typeof(MeansImplicitUseAttribute))]
    public class When_instantiating_used_implicitly_attribute_specifying_target
    {
        private Because Of = () => _expectedAttribute = new UsedImplicitlyAttribute(ImplicitUseTargetFlags.WithMembers);

        private It Should_use_default_value_for_kind
            = () => _expectedAttribute.UseKindFlags.ShouldEqual(ImplicitUseKindFlags.Default);

        private It Should_use_specified_value_for_target
            = () => _expectedAttribute.TargetFlags.ShouldEqual(ImplicitUseTargetFlags.WithMembers);

        private static UsedImplicitlyAttribute _expectedAttribute;
    }
}