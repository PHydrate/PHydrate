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

        public string Noun
        {
            get { return GetWaffle( "|o" ); }
        }

        public int Integer
        {
            get { return _random.Next(); }
        }

        public bool Boolean
        {
            get { return _random.NextDouble() >= 0.5; }
        }

        public byte Byte
        {
            get
            {
                var bytes = new byte[1];
                _random.NextBytes( bytes );
                return bytes[0];
            }
        }

        public char Character
        {
            get { return Password[0]; }
        }

        public static Guid Guid
        {
            get { return Guid.NewGuid(); }
        }

        public short Short
        {
            get { return (short)_random.Next(); }
        }

        public long LongInteger
        {
            get { return _random.Next(); }
        }

        public float Float
        {
            get { return (float)(_random.NextDouble() * _random.Next()); }
        }

        public double Double
        {
            get { return (_random.NextDouble() * _random.Next()); }
        }

        public decimal Decimal
        {
            get { return (decimal)(_random.NextDouble() * _random.Next()); }
        }

        public DateTime DateTime
        {
            get { return new DateTime( _random.Next( 1970, 2100 ), _random.Next( 1, 12 ), _random.Next( 1, 28 ) ); }
        }

        string GetWaffle( string phrase )
        {
            var stringBuilder = new StringBuilder();
            _waffleEngine.EvaluatePhrase( phrase, stringBuilder );
            return stringBuilder.ToString();
        }
    }
}
