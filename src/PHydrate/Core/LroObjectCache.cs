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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PHydrate.Core
{
    internal sealed class LroObjectCache< TIdentifierType > : IObjectCache< TIdentifierType >
    {
        private readonly IDictionary< Type, IDictionary< TIdentifierType, TimeStampedObjectWrapper > > _cache =
            new ConcurrentDictionary< Type, IDictionary< TIdentifierType, TimeStampedObjectWrapper > >();

        internal LroObjectCache( int maxCacheSize )
        {
            _maxCacheSize = maxCacheSize;
        }

        private readonly int _maxCacheSize;
        private int _cacheSize;
        private readonly object _cacheLock = new object();

        /// <summary>
        /// Determines whether the object exists in the cache
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="identifier">The identifier.</param>
        /// <returns>
        ///   <c>true</c> the object exists; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInCache< T >( TIdentifierType identifier )
        {
            return _cache.ContainsKey( typeof(T) ) && _cache[ typeof(T) ].ContainsKey( identifier );
        }

        /// <summary>
        /// Gets from cache.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="identifier">The identifier.</param>
        /// <returns>The object from the cache</returns>
        public T GetFromCache<T>(TIdentifierType identifier)
        {
            if ( IsInCache< T >( identifier ) )
            {
                _cache[ typeof(T) ][ identifier ].Touch();
                return (T)_cache[ typeof(T) ][ identifier ].WrappedObject;
            }

            throw new PHydrateInternalException( "Could not find key in cache.  Code should call IsInCache() first." );
        }

        /// <summary>
        /// Adds to the cache.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="identifier">The identifier.</param>
        /// <param name="obj">The object to store.</param>
        public void AddToCache<T>(TIdentifierType identifier, T obj)
        {
            if ( !_cache.ContainsKey( typeof(T) ) )
                _cache[ typeof(T) ] = new ConcurrentDictionary< TIdentifierType, TimeStampedObjectWrapper >();
            _cache[ typeof(T) ].Add( identifier, new TimeStampedObjectWrapper( obj ) );
            lock ( _cacheLock )
                _cacheSize++;
        }

        /// <summary>
        /// Removes old entries from the cache.
        /// </summary>
        public void Cleanup()
        {
            var orderedList = _cache.SelectMany( x => x.Value ).OrderBy( x => x.Value.LastAccessed ).GetEnumerator();
            while ( _cacheSize > _maxCacheSize && orderedList.MoveNext() )
                RemoveItem( orderedList.Current );
        }

        private void RemoveItem( KeyValuePair< TIdentifierType, TimeStampedObjectWrapper > kvp )
        {
            //var item = new WeakReference( kvp.Value.WrappedObject );
            lock ( _cacheLock )
            {
                _cache[ kvp.Value.WrappedObject.GetType() ].Remove( kvp.Key );
                _cacheSize--;
            }
            //kvp.Value.ReleaseWrappedObject();
            //if ( !item.IsAlive && item.Target is IDisposable )
            //    ( (IDisposable)item.Target ).Dispose();

        }

        private sealed class TimeStampedObjectWrapper
        {
            public TimeStampedObjectWrapper( object o )
            {
                WrappedObject = o;
                Touch();
            }

            public void Touch()
            {
                LastAccessed = DateTime.Now;
            }

            public DateTime LastAccessed { get; private set; }
            public Object WrappedObject { get; private set; }

            //public void ReleaseWrappedObject()
            //{
            //    WrappedObject = null;
            //}
        }
    }
}