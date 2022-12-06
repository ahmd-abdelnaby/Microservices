using Logging;
using MassTransitConsumer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OrderApplication.Extentions;
using ProductAppliction.Consumers;
using ProductAppliction.Models;
using ProductOrderMessage;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddTransient<LoggingService>();
        webApplicationBuilder.AddMediatR();
        webApplicationBuilder.AddHealthCheck();

        webApplicationBuilder.Services.AddCustomMassTransitConsumer<OrderConsumer<ProductOrderMessageModel>, ProductOrderMessageModel>("Development");//,webApplicationBuilder.Environment


        webApplicationBuilder.AddAuthoriz();

        webApplicationBuilder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        webApplicationBuilder.Services.AddControllers();
        webApplicationBuilder.Services.AddEndpointsApiExplorer();
        return webApplicationBuilder;
    }
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseAuthoriz();
        return app;
    }
}