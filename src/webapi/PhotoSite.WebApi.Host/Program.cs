using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;

namespace PhotoSite.WebApi
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Run();
        }

        /// <summary>
        /// CreateHostBuilder
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <returns></returns>
        public static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            })
            .ConfigureLogging((context, builder) =>
            {
                builder.ClearProviders();
                builder.AddSerilog(new LoggerConfiguration()
                    .ReadFrom.Configuration(context.Configuration)
                    .CreateLogger());
            })
            .UseStartup<Startup>()
            .Build();
    }
}
