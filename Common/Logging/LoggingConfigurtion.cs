using Logging.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SpectreConsole;
using System.Text;
namespace Logging
{
    public static class LoggingConfigurtion
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
           (hostingContext, loggerConfiguration) =>
           {
               var env = hostingContext.HostingEnvironment;
               var logOptions = hostingContext.Configuration.GetSection(nameof(LogOptions)).Get<LogOptions>();
               var logLevel = Enum.TryParse<LogEventLevel>(logOptions.Level, true, out var level)
                   ? level
                   : LogEventLevel.Information;


               loggerConfiguration.MinimumLevel.Is(logLevel)
                   .WriteTo.SpectreConsole(logOptions.LogTemplate, logLevel)
                   .Enrich.FromLogContext()
                   .Enrich.WithProperty("ApplicationName", env.ApplicationName)
                   .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                   .Enrich.WithExceptionDetails()
                   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                   .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
                   .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information);

               if (hostingContext.HostingEnvironment.IsDevelopment())
               {
                   loggerConfiguration.MinimumLevel.Override("GloboTicket", LogEventLevel.Debug);
               }

               if (logOptions.Elastic is { Enable: true })
               {
                   loggerConfiguration.WriteTo.Elasticsearch(
                       new ElasticsearchSinkOptions(new Uri(logOptions.Elastic.ElasticServiceUrl))
                       {
                           AutoRegisterTemplate = true,
                           AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                           IndexFormat = $"{env.ApplicationName}-{env.EnvironmentName?.ToLower()}",
                       });
               }
               if (logOptions.File is { Enable: true })
               {
                   var root = env.ContentRootPath;
                   Directory.CreateDirectory(Path.Combine(root, "logs"));

                   var path = string.IsNullOrWhiteSpace(logOptions.File.Path) ? "logs/.txt" : logOptions.File.Path;
                   if (!Enum.TryParse<RollingInterval>(logOptions.File.Interval, true, out var interval))
                   {
                       interval = RollingInterval.Day;
                   }

                   loggerConfiguration.WriteTo.File(path, rollingInterval: interval, encoding: Encoding.UTF8, outputTemplate: logOptions.LogTemplate);
               }
           };
    }
}