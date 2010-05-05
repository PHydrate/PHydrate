using System;
using System.Text;

namespace SMC.TestingUtils
{
    public class RandomDataGenerator
    {
        readonly Random _random;
        readonly WaffleEngine _waffleEngine;

        protected internal RandomDataGenerator()
        {
            _random = new Random();
            _waffleEngine = new WaffleEngine( _random );
        }

        public string FirstName
        {
            get { return GetWaffle( "|f" ); }
        }

        public string LastName
        {
            get { return GetWaffle( "|s" ); }
        }

        public string Password
        {
            get { return GetWaffle( "|ue|ud" ); }
        }

        string GetWaffle( string phrase )
        {
            var stringBuilder = new StringBuilder();
            _waffleEngine.EvaluatePhrase( phrase, stringBuilder );
            return stringBuilder.ToString();
        }
    }
}
