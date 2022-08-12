using Microsoft.AspNetCore.Builder;
using PhotoSite.WebApi.Middlewares;

namespace PhotoSite.WebApi.Helpers
{
    /// <summary>
    /// Middleware extensions
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Use IpFilter
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseIpFilter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpFilter>();
        }

        /// <summary>
        /// Use ExceptionHandler
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
        
    }
}