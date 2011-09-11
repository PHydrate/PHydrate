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
    internal sealed class LroObjectCache : IObjectCache
    {
        private readonly IDictionary< Type, IDictionary< int, TimeStampedObjectWrapper > > _cache =
            new ConcurrentDictionary< Type, IDictionary< int, TimeStampedObjectWrapper > >();

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
        /// <param name="hashCode">The hash code.</param>
        /// <returns>
        ///   <c>true</c> the object exists; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInCache< T >( int hashCode )
        {
            return _cache.ContainsKey( typeof(T) ) && _cache[ typeof(T) ].ContainsKey( hashCode );
        }

        /// <summary>
        /// Gets from cache.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="hashCode">The hash code.</param>
        /// <returns>The object from the cache</returns>
        public T GetFromCache<T>(int hashCode)
        {
            if ( IsInCache< T >( hashCode ) )
            {
                _cache[ typeof(T) ][ hashCode ].Touch();
                return (T)_cache[ typeof(T) ][ hashCode ].WrappedObject;
            }

            throw new PHydrateInternalException( "Could not find object in cache.  Code should call IsInCache() first." );
        }

        /// <summary>
        /// Adds to the cache.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="hashCode">The hash code.</param>
        /// <param name="obj">The object to store.</param>
        public void AddToCache<T>(int hashCode, T obj)
        {
            if ( !_cache.ContainsKey( typeof(T) ) )
                _cache[ typeof(T) ] = new ConcurrentDictionary< int, TimeStampedObjectWrapper >();
            _cache[ typeof(T) ].Add( hashCode, new TimeStampedObjectWrapper( obj ) );
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

        private void RemoveItem( KeyValuePair< int, TimeStampedObjectWrapper > kvp )
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