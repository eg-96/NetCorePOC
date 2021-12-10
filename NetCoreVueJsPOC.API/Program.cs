using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreVueJsPOC.DAL;
using NetCoreVueJsPOC.Utilities;
using NLog;
using NLog.Web;

namespace NetCoreVueJsPOC.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NLog: setup the logger first to catch all errors
            var logger = LoggerUtils.GetLogger();

            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    logger.Debug("Init Main");

                    var context = services.GetRequiredService<NetCoreVueJsPOCContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred while seeding the database");
                }
            }

            try
            {
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while starting the application");
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();  // NLog: setup NLog for Dependency injection
    }
}
