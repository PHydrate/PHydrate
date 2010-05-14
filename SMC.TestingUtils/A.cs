namespace SMC.TestingUtils
{
    public static class A
    {
        static readonly RandomDataGenerator RandomDataGenerator;

        static A()
        {
            RandomDataGenerator = new RandomDataGenerator();
        }

        public static RandomDataGenerator Random
        {
            get { return RandomDataGenerator; }
        }
    }
}
