using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PhotoSite.ApiService.Base;
using PhotoSite.Core.Cache;

namespace PhotoSite.ApiService
{
    public static class ServiceProviderExtensions
    {
        private static IEnumerable<Type> GetImplementedTypes<T>()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        }

        /// <summary>
        /// Register all api services
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiServices(this IServiceCollection services)
        {
            var serviceTypes = GetImplementedTypes<IService>();
            foreach (var serviceType in serviceTypes)
            {
                var serviceInterface = serviceType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t => t != typeof(IService) && !t.IsGenericType);
                if (serviceInterface == null)
                    throw new Exception($"Service '{serviceType.FullName}' not implemented specific interface");
                services.TryAddScoped(serviceInterface, serviceType);
            }
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
                services.TryAddScoped(cacheInterface, cacheType);
            }
        }
        
    }
}
