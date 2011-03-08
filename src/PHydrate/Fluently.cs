#region Copyright

// This file is part of PHydrate.
// 
// PHydrate is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// PHydrate is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with PHydrate.  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright 2010, Stephen Michael Czetty
// 

#endregion

using PHydrate.Core;

namespace PHydrate
{
    /// <summary>
    /// Entry point for PHydrate configuration
    /// </summary>
    /// <remarks>
    /// Use Fluently.Configure to access a <see cref="FluentConfiguration"/> instance to complete the configuration.
    /// <example>
    /// <code>
    /// <see cref="ISessionFactory"/> sessionFactory = <see cref="Fluently"/>.Configure.Database(new <see cref="SqlServerDatabaseService"/>(...)).ParameterPrefix("@").BuildSessionFactory();
    /// </code>
    /// </example>
    /// </remarks>
    public static class Fluently
    {
        /// <summary>
        /// Gets a <see cref="FluentConfiguration"/> instance for configuring PHydrate
        /// </summary>
        public static FluentConfiguration Configure
        {
            get { return new FluentConfiguration(); }
        }
    }
}