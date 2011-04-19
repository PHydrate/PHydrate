using Machine.Specifications;

namespace PHydrate.Specs.Behaviors
{
    public class FluentInterfaceBase
    {
        protected static object FluentInterface;
        protected static object ReturnedObject;
    }

    [Behaviors]
    public class AFluentInterface : FluentInterfaceBase
    {
        private It Should_return_the_same_object
            = () => ReturnedObject.ShouldBeTheSameAs(FluentInterface);
    }
}