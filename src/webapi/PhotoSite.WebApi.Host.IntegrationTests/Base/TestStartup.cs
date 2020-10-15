using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace PhotoSite.WebApi.Host.IntegrationTests.Base
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IWebHostEnvironment currentEnvironment) : base(configuration, currentEnvironment)
        {
        }

        //public override void ConfigureDependencies(IServiceCollection services)
        //{
        //    base.ConfigureDependencies(services);
        //}
    }
}