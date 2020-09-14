using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PhotoSite.ApiService.Base;

namespace PhotoSite.ApiService
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Register all api services
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiServices(this IServiceCollection services)
        {
            var serviceTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IService).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
            foreach (var serviceType in serviceTypes)
            {
                var serviceInterface = serviceType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t => t != typeof(IService));
                if (serviceInterface == null)
                    throw new Exception($"Service '{serviceType.FullName}' not implemented specific interface");
                services.TryAddSingleton(serviceInterface, serviceType);
            }
        }
    }
}
