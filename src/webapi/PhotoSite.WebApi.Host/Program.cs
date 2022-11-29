using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;
using PhotoSite.WebApi.Infrastructure;
using System.Threading.Tasks;
using PhotoSite.ApiService.Base;
using PhotoSite.Domain.Admin;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PhotoSite.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .CustomConfigureAppConfiguration(Directory.GetCurrentDirectory())
                .ConfigureLogging((context, builder) =>
                {
                    builder.ClearProviders();
                    builder.AddSerilog(new LoggerConfiguration()
                        .ReadFrom.Configuration(context.Configuration)
                        .CreateLogger());
                })
                .UseStartup<Startup>();
    }
}
