using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace PhotoSite.WebApi.Host.IntegrationTests.Base
{
    public class FakeRemoteIpAddressStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<FakeRemoteIpAddressMiddleware>();
                next(app);
            };
        }
    }
}