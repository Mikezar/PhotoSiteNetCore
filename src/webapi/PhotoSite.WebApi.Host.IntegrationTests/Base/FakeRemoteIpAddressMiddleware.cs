using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PhotoSite.WebApi.Host.IntegrationTests.Base
{
    public class FakeRemoteIpAddressMiddleware
    {
        public const string FakeIpAddressHeaderName = "FakeIpAddress";
        private readonly RequestDelegate _next;

        public FakeRemoteIpAddressMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.ContainsKey(FakeIpAddressHeaderName))
            {
                var fakeIpAddressString = httpContext.Request.Headers[FakeIpAddressHeaderName];
                var fakeIpAddress = IPAddress.Parse(fakeIpAddressString); // ex:"127.168.1.32"
                httpContext.Connection.RemoteIpAddress = fakeIpAddress;
            }

            await _next(httpContext);
        }
    }
}