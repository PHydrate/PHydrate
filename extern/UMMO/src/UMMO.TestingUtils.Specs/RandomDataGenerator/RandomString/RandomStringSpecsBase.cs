using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomString
{
    public class RandomStringSpecsBase : RandomDataGeneratorTestBase {
        private Establish Context =()=> RandomString = RandomDataGeneratorUnderTest.String;
        protected static object RandomString;
    }
}