using Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Authentication;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.Authority = "https://localhost:5000";
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
        services.AddTransient<LoggingService>();
        services.AddTransient<LoggingService>();


        return services;
    }
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseHealthChecks("/hc", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        return app;
    }
    }