using Machine.Specifications;
using Machine.Specifications.Annotations;
using PHydrate.Core;
using Rhino.Mocks;

namespace PHydrate.Specs.Core.SessionFactory
{
    public class SessionFactorySpecificationBase
    {
        [ UsedImplicitly ]
        private Establish Context = () => {
                                        _databaseService = MockRepository.GenerateStub< IDatabaseService >();
                                        SessionFactoryUnderTest = new PHydrate.Core.SessionFactory(_databaseService);
                                    };

        protected static ISessionFactory SessionFactoryUnderTest;
        private static IDatabaseService _databaseService;
    }
}