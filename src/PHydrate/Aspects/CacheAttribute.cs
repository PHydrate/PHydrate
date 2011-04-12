using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PHydrate.Util;
using PostSharp.Aspects;

namespace PHydrate.Aspects
{
    /// <summary>
    /// Aspect enabling caching of the return value from methods.
    /// </summary>
    [Serializable]
    public sealed class CacheAttribute : OnMethodBoundaryAspect
    {
        private readonly IDictionary< int, object > _methodResultCache = new Dictionary< int, object >();
        private Type _methodType;

        /// <summary>
        /// Method invoked at build time to initialize the instance fields of the current aspect. This method is invoked
        ///             before any other build-time method.
        /// </summary>
        /// <param name="method">Method to which the current aspect is applied</param><param name="aspectInfo">Reserved for future usage.</param>
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            _methodType = method.DeclaringType;
        }

        private int GetCacheKey(MethodExecutionArgs args)
        {
            return
                _methodType.GetObjectsHashCodeByFieldValues(
                    new[] { args.Instance }.Concat(
                        ( args.Method.GetGenericArguments() ).Select( x => (object)x.FullName ) ).Concat(
                            args.Arguments.ToArray() ).ToArray() );
        }

        /// <summary>
        /// Method executed <b>before</b> the body of methods to which this aspect is applied.
        /// </summary>
        /// <param name="args">Event arguments specifying which method
        ///             is being executed, which are its arguments, and how should the execution continue
        ///             after the execution of <see cref="M:PostSharp.Aspects.IOnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)"/>.</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            int cacheKey = GetCacheKey( args );

            if (_methodResultCache.ContainsKey(cacheKey))
            {
                args.ReturnValue = _methodResultCache[cacheKey];
                args.FlowBehavior = FlowBehavior.Return;
            }
            else
                args.MethodExecutionTag = cacheKey;
        }

        /// <summary>
        /// Method executed <b>after</b> the body of methods to which this aspect is applied,
        ///             but only when the method successfully returns (i.e. when no exception flies out
        ///             the method.).
        /// </summary>
        /// <param name="args">Event arguments specifying which method
        ///             is being executed and which are its arguments.</param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            var cacheKey = (int)args.MethodExecutionTag;
            _methodResultCache[ cacheKey ] = args.ReturnValue;
        }
    }
}