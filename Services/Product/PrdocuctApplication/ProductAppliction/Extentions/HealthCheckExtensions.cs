using HealthChecks.System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks()
                 .AddDiskStorageHealthCheck(delegate (DiskStorageOptions diskStorageOptions)
                 {
                     diskStorageOptions.AddDrive(@"C:\", 500000000000000000);
                 }, name: "My Drive", HealthStatus.Unhealthy);

        return services;
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