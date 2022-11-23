using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace Logging
{
    public static class LoggingConfigurtion
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
           (hostingContext, loggerConfiguration) =>
           {
               var env = hostingContext.HostingEnvironment;

               loggerConfiguration.MinimumLevel.Information()
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

               var elasticUrl = hostingContext.Configuration.GetValue<string>("Logging:ElasticUrl");

               if (!string.IsNullOrEmpty(elasticUrl))
               {
                   //loggerConfiguration.WriteTo.Elasticsearch(
                   //    new ElasticsearchSinkOptions(new Uri(elasticUrl))
                   //    {
                   //        AutoRegisterTemplate = true,
                   //        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                   //        IndexFormat = "Micro-Services-logs-{0:yyyy.MM.dd}",
                   //        MinimumLogEventLevel = LogEventLevel.Debug
                   //    });
               }
           };
    }
}