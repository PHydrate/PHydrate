using PHydrate.Attributes;

namespace PHydrate.Tests.Integration.TestDomain
{
    [HydrateUsing("GetSimple")]
    [CreateUsing("CreateSimple")]
    public class Simple
    {
        [PrimaryKey]
        public long SimpleId { get; private set; }

        public long IntegerValue { get; set; }

        public string StringValue { get; set; }
    }
}