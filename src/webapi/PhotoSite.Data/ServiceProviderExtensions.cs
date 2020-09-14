using Microsoft.Extensions.DependencyInjection;
using PhotoSite.Data.Base;

namespace PhotoSite.Data
{
    public static class ServiceProviderExtensions
    {
        public static void AddData(this IServiceCollection services)
        {
            services.AddTransient<DbFactory>();
        }
    }
}
