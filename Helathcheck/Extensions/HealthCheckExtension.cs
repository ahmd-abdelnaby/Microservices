using HealthChecks.System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Claims;

namespace Helathcheck.Extensions
{
    public static class HealthCheckExtension
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, WebApplicationBuilder webApplicationBuilder)
        {
            services.AddHealthChecks()
                .AddDiskStorageHealthCheck(delegate (DiskStorageOptions diskStorageOptions)
                {
                    diskStorageOptions.AddDrive(@"C:\", 5000);
                }, name: "My Drive", HealthStatus.Unhealthy)
                .AddSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            services.AddHealthChecksUI().AddInMemoryStorage();
            return services;
        }
        public static WebApplication UseHealthCheck(this WebApplication app)
        {
            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }).UseHealthChecksUI(options =>
            {
                options.UIPath = "/hc-ui";
                options.AddCustomStylesheet("./Customization/custom.css");
            });
            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            return app;
        }
    }
}
