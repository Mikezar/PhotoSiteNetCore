using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using ILogger = Serilog.ILogger;

namespace PhotoSite.WebApi.Infrastructure
{
    public static class HostConfigurator
    {
        private static readonly ILogger Logger = new LoggerConfiguration().CreateLogger();

        public static IWebHostBuilder CustomConfigureAppConfiguration(this IWebHostBuilder hostBuilder, string basePath)
        {
            hostBuilder.ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            });

            return hostBuilder;
        }

        public static void SetupExceptionHandlers()
        {
            // Unhandled exception
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Logger.Error(args.ExceptionObject as Exception, "UnhandledException");
            };

            // Work only by compile to Release
            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                args.SetObserved();
                args.Exception?.Handle(ex =>
                {
                    Logger.Error(ex, "UnobservedTaskException");
                    return true;
                });
            };
        }

    }
}