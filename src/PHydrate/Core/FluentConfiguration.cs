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
// Copyright 2010-2011, Stephen Michael Czetty

#endregion

namespace PHydrate.Core
{
    /// <summary>
    ///   Configuration class for PHydrate
    /// </summary>
    public class FluentConfiguration
    {
        private IDatabaseServiceProvider _databaseServiceProvider;
        private string _prefix = "@";
        private IDefaultObjectHydrator _defaultObjectHydrator;
        private int _cacheSize;

        internal FluentConfiguration() {}

        /// <summary>
        ///   Specifies the database service to use.
        /// </summary>
        /// <param name = "databaseService">The database service.</param>
        /// <returns></returns>
        public FluentConfiguration DatabaseProvider( IDatabaseServiceProvider databaseService )
        {
            _databaseServiceProvider = databaseService;
            return this;
        }

        /// <summary>
        ///   Specifies a string to prepend to parameter names.  Defaults to "@".
        /// </summary>
        /// <param name = "prefix">The prefix.</param>
        /// <returns></returns>
        public FluentConfiguration ParameterPrefix( string prefix )
        {
            _prefix = prefix;
            return this;
        }

        /// <summary>
        ///   Withes the default hydrator.  If not specified, the built-in hydrator is used.
        /// </summary>
        /// <param name = "defaultObjectHydrator">The default object hydrator.</param>
        /// <returns></returns>
        public FluentConfiguration WithDefaultHydrator( IDefaultObjectHydrator defaultObjectHydrator )
        {
            _defaultObjectHydrator = defaultObjectHydrator;
            return this;
        }

        /// <summary>
        ///   Sets the size of the cache.
        /// </summary>
        /// <param name = "cacheSize">Size of the cache.</param>
        /// <returns></returns>
        public FluentConfiguration WithCacheSize( int cacheSize )
        {
            _cacheSize = cacheSize;
            return this;
        }

        /// <summary>
        ///   Builds the session factory.
        /// </summary>
        /// <returns></returns>
        public ISessionFactory BuildSessionFactory()
        {
            return new SessionFactory( _databaseServiceProvider, _prefix, _defaultObjectHydrator,
                                       new LroObjectCache( _cacheSize ) );
        }
    }
}