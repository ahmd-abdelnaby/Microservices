using HealthChecks.UI.Client;
using Logging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
    public static WebApplication UseMediatR(this WebApplication app)
    {
        return app;

    }
}