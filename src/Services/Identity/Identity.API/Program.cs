using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Identity.API
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
                host.MigrateDbContext<AccountContext>();

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
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .UseUrls("http://localhost:57045")//HACK: CONFIG FILE TROUBLE (ignores port)
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

    public static class IWebHostExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost host) where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                context.Database.Migrate();

                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            }

            return host;
        }
    }
}
