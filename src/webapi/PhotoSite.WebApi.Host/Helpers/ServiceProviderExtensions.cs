using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PhotoSite.Core.Cache;

namespace PhotoSite.WebApi.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceProviderExtensions
    {

        private static IEnumerable<Type> GetImplementedTypes<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
                a.GetTypes().Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract));
        }

        /// <summary>
        /// Register all api caches
        /// </summary>
        /// <param name="services"></param>
        public static void AddCaches(this IServiceCollection services)
        {
            var cacheTypes = GetImplementedTypes<ICache>();
            foreach (var cacheType in cacheTypes)
            {
                var cacheInterface = cacheType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t =>
                    t != typeof(ICache) && t != typeof(IDisposable) && !t.IsGenericType);
                if (cacheInterface == null)
                    throw new Exception($"Cache '{cacheType.FullName}' not implemented specific interface");
                services.TryAddSingleton(cacheInterface, cacheType);
            }
        }

        /// <summary>
        /// Register all cache providers
        /// </summary>
        /// <param name="services"></param>
        public static void AddCacheProvider(this IServiceCollection services)
        {
            var cacheTypes = GetImplementedTypes<ICacheProvider>();
            foreach (var cacheType in cacheTypes)
                services.TryAddSingleton(cacheType);
        }

    }
}