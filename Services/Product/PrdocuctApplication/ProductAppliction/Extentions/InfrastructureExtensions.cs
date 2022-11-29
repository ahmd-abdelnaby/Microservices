using Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<LoggingService>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.Authority = "https://localhost:5000"; // identity
            o.Audience = "resourceapi";
            o.RequireHttpsMetadata = false;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiReader", policy => policy.RequireClaim("scope", "api.read"));
            options.AddPolicy("Consumer", policy => policy.RequireClaim(ClaimTypes.Role, "consumer"));
        }
        );
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddHealthChecks()
                .AddDiskStorageHealthCheck(delegate (DiskStorageOptions diskStorageOptions)
                {
                    diskStorageOptions.AddDrive(@"C:\", 500000000000000000);
                }, name: "My Drive", HealthStatus.Unhealthy);

        return services;
    }
}