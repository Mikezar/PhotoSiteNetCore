using System;
using System.Threading.Tasks;
using Serilog;

namespace PhotoSite.WebApi.Configuration
{
    /// <summary>
    /// Host configurator
    /// </summary>
    public static class HostConfigurator
    {
        private static readonly ILogger Logger = new LoggerConfiguration().CreateLogger();

        /// <summary>
        /// Setup exception handlers
        /// </summary>
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