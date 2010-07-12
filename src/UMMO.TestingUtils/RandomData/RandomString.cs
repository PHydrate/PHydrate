using System;
using System.Text;
using UMMO.TestingUtils.RandomData.Waffle;

namespace UMMO.TestingUtils.RandomData
{
    public class RandomString
    {
        private readonly Random _random;
        private readonly WaffleEngine _waffleEngine;

        protected internal RandomString(Random random)
        {
            _random = random;
            _waffleEngine = new WaffleEngine( _random );
        }

        // Methods for fluency
        public RandomString Resembling { get { return this; } }
        public RandomString A { get { return this; } }
        //public RandomString An { get { return this; } }

        public string FirstName
        {
            get { return GetWaffle("|f"); }
        }

        public string LastName
        {
            get { return GetWaffle("|s"); }
        }

        public string Password
        {
            get { return GetWaffle("|ue|ud"); }
        }

        public string Noun
        {
            get { return GetWaffle("|o"); }
        }

        public static implicit operator string(RandomString randomString)
        {
            return randomString.Noun;
        }

        private string GetWaffle(string phrase)
        {
            var stringBuilder = new StringBuilder();
            _waffleEngine.EvaluatePhrase(phrase, stringBuilder);
            return stringBuilder.ToString();
        }
    }
}