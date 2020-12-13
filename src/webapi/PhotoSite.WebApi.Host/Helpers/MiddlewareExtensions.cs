using Microsoft.AspNetCore.Builder;
using PhotoSite.WebApi.Filters;

namespace PhotoSite.WebApi.Helpers
{
    /// <summary>
    /// Middleware extensions
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// UseIpFilter
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseIpFilter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpFilter>();
        }
    }
}