using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PhotoSite.ManagementBoard.Services.Abstract;
using System;
using System.Linq;
using System.Reflection;

namespace PhotoSite.ManagementBoard.Services.Implementation
{
    public static class ServiceExtensions
    {

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            var implementationTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IService).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

            foreach(var implementationType in implementationTypes)
            {
                var serviceInterface = implementationType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t =>
                    t != typeof(IService) && t != typeof(IDisposable) && !t.IsGenericType);

                if (serviceInterface == null)
                    throw new Exception($"Cache '{serviceInterface.FullName}' not implemented specific interface");

                services.TryAddScoped(serviceInterface, implementationType);
            }

            return services;
        }
    }
}
