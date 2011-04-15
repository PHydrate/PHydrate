using Machine.Specifications;
using PHydrate.Specs.Behaviors;
using Rhino.Mocks;

namespace PHydrate.Specs.Core.FluentConfiguration
{
    [Subject(typeof(PHydrate.Core.FluentConfiguration))]
    public sealed class When_setting_default_hydrator : FluentConfigurationSpecificationBase
    {
        private static IDefaultObjectHydrator _defaultObjectHydrator;

        private Establish Context =
            () => _defaultObjectHydrator = MockRepository.GenerateStub<IDefaultObjectHydrator>();

        private Because Of = () => ReturnedObject = FluentConfiguration.WithDefaultHydrator(_defaultObjectHydrator);

        private Behaves_like<AFluentInterface> a_fluent_interface;
    }
}