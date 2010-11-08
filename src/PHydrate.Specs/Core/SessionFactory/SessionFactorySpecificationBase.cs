using Machine.Specifications;
using Machine.Specifications.Annotations;

namespace PHydrate.Specs.Core.SessionFactory
{
    public class SessionFactorySpecificationBase
    {
        [UsedImplicitly]
        private Establish Context = () => SessionFactoryUnderTest = new PHydrate.Core.SessionFactory();
        protected static ISessionFactory SessionFactoryUnderTest;
    }
}