using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PhotoSite.Domain.Admin;

namespace PhotoSite.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(typeof(ServiceCollectionExtensions));
        serviceCollection.AddSingleton<ITokenManager, TokenManager>();
        serviceCollection.AddSingleton<IUserAuthentication, UserAuthentication>();
        return serviceCollection;
    }
}

