using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;
using PhotoSite.WebApi.Infrastructure;

namespace PhotoSite.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostConfigurator.SetupExceptionHandlers();
            CreateHostBuilder(args).Build().Run();
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
