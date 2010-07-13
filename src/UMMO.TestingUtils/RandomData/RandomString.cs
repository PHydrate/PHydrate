#region Copyright

// This file is part of UMMO.
// 
// UMMO is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// UMMO is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with UMMO.  If not, see <http://www.gnu.org/licenses/>.
//  
// Copyright 2010, Stephen Michael Czetty

#endregion

using System.Text;
using UMMO.TestingUtils.RandomData.Waffle;

namespace UMMO.TestingUtils.RandomData
{
    public class RandomString
    {
        private readonly IRandom _random;
        private readonly WaffleEngine _waffleEngine;

        protected internal RandomString( IRandom random )
        {
            _random = random;
            _waffleEngine = new WaffleEngine( _random );
        }

        // Methods for fluency
        public RandomString Resembling
        {
            get { return this; }
        }

        public RandomString A
        {
            get { return this; }
        }

        //public RandomString An { get { return this; } }

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

        public static implicit operator string( RandomString randomString )
        {
            return randomString.Noun;
        }

        private string GetWaffle( string phrase )
        {
            var stringBuilder = new StringBuilder();
            _waffleEngine.EvaluatePhrase( phrase, stringBuilder );
            return stringBuilder.ToString();
        }
    }
}