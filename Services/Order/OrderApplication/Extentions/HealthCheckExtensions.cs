using HealthChecks.System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public static class HealthCheckExtensions
{
    public static WebApplicationBuilder AddHealthCheck(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddHealthChecks()
                 .AddDiskStorageHealthCheck(delegate (DiskStorageOptions diskStorageOptions)
                 {
                     diskStorageOptions.AddDrive(@"C:\", 500000000000000000);
                 }, name: "My Drive", HealthStatus.Unhealthy);

        return webApplicationBuilder;
    }
    public static WebApplication UseHealthCheck(this WebApplication app)
    {
        app.UseHealthChecks("/hc", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}