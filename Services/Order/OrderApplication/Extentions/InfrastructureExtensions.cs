using Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OrderApplication.Extentions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatR();
        services.AddHealthCheck();
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddAuthoriz();
        services.AddTransient<LoggingService>();
        return services;
    }
    public static WebApplication UseInfrastructure(this WebApplication app)
    {

        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseAuthoriz();

        return app;
    }
}