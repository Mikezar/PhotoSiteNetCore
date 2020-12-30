using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PhotoSite.Data.Base;

namespace PhotoSite.Data
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Register all api repositories
        /// </summary>
        /// <param name="services"></param>
        public static void AddRepositories(this IServiceCollection services)
        {
            var repositoryTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IRepository).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
            foreach (var repositoryType in repositoryTypes)
            {
                var repositoryInterface = repositoryType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t => t != typeof(IRepository) && t != typeof(IDbContext) && !t.IsGenericType);
                if (repositoryInterface == null)
                    throw new Exception($"Repository '{repositoryType.FullName}' not implemented specific interface");
                services.TryAddScoped(repositoryInterface, repositoryType);
            }
        }
    }
}