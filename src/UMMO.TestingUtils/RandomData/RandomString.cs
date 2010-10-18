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
    /// <summary>
    /// Fluent random string.
    /// </summary>
    public class RandomString
    {
        private readonly IRandom _random;
        private readonly WaffleEngine _waffleEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomString"/> class.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public RandomString( IRandom random )
        {
            _random = random;
            _waffleEngine = new WaffleEngine( _random );
        }

        // Methods for fluency
        /// <summary>
        /// Syntatic sugar for fluent interface.  Returns instance.
        /// </summary>
        /// <value>This instance.</value>
        public RandomString Resembling
        {
            get { return this; }
        }

        /// <summary>
        /// Syntatic sugar for fluent interface.  Returns instance.
        /// </summary>
        /// <value>This instance.</value>
        public RandomString A
        {
            get { return this; }
        }

        //public RandomString An { get { return this; } }

        /// <summary>
        /// A random first name
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get { return GetWaffle( "|f" ); }
        }

        /// <summary>
        /// A random last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get { return GetWaffle( "|s" ); }
        }

        /// <summary>
        /// A random password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get { return GetWaffle( "|ue|ud" ); }
        }

        /// <summary>
        /// A random noun.
        /// </summary>
        /// <value>The noun.</value>
        public string Noun
        {
            get { return GetWaffle( "|o" ); }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UMMO.TestingUtils.RandomData.RandomString"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="randomString">The random string.</param>
        /// <returns>The result of the conversion.</returns>
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