using Microsoft.AspNetCore.Builder;
using PhotoSite.WebApi.Middlewares;

namespace PhotoSite.WebApi.Infrastructure.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseIpFilter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpFilter>();
        }

        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}