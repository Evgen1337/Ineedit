using Ads.API.Infrastructure;
using Ads.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using WebHostBlocks.Customization;

namespace Ads.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = CreateSerilogLogger();

            try
            {
                Log.Information("Configuring web host...");
                var host = BuildWebHost(configuration, args);

                Log.Information("Applying migrations...");
                host.MigrateDbContext<AdsContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<AdContextSeed>>();

                    new AdContextSeed()
                       .SeedAsync(context, logger)
                       .Wait();
                });

                Log.Information("Starting web host ...");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseIIS()
                .UseIISIntegration()
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .UseUrls("http://localhost:53355")//HACK: CONFIG FILE TROUBLE (ignores port)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSerilog()
                .Build();

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private static Serilog.ILogger CreateSerilogLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
#if DEBUG
                .WriteTo.RollingFile(@"bin\logs\log.log", retainedFileCountLimit: 7)
#else
                .WriteTo.RollingFile(@"logs\log.log", retainedFileCountLimit: 7)
#endif
                .CreateLogger();
        }
    }
}
