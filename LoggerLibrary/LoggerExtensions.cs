using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibrary
{
    public static class LoggerExtensions
    {
        public static IHostBuilder UseSerilogLogger(this IHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            
            SerilogHostBuilderExtensions.UseSerilog(builder);
            return builder;
        }

        public static WebApplication SerilogPipelineConfig<T>(this WebApplication builder) where T : class
        {
            using var scope = builder.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<T>();
            try
            {
                logger.LogInformation("Application just started here!!!");
            }
            catch (Exception ex)
            {
                logger.LogCritical("Application failed fatally at startup!!!");
                logger.LogCritical(ex, ex.Message);
            }
            builder.UseSerilogRequestLogging();
            return builder;
        }
    }
}
