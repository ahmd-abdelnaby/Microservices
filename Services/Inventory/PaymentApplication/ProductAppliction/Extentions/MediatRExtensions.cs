using Logging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class MediatRExtensions
{
    public static WebApplicationBuilder AddMediatR(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddMediatR(Assembly.GetExecutingAssembly());
        webApplicationBuilder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return webApplicationBuilder;
    }
    public static WebApplication UseMediatR(this WebApplication app)
    {
        return app;
    }
}