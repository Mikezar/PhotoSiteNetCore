using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PhotoSite.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(typeof(ServiceCollectionExtensions));
        return serviceCollection;
    }
}

