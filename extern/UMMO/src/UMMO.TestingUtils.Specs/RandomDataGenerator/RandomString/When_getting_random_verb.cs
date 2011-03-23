using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomString
{
    [Subject(typeof(RandomData.RandomString))]
    public class When_getting_random_verb : RandomStringSpecsBase
    {
        private Because Of = () => _randomVerb = ((RandomData.RandomString)RandomString).Verb;

        private It Should_not_be_null
            = () => _randomVerb.ShouldNotBeNull();

        private It Should_not_be_empty
            = () => _randomVerb.Length.ShouldBeGreaterThan( 0 );

        private static string _randomVerb;
    }
}