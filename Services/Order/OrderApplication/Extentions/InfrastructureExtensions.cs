using Logging;
using MassTransitConsumer;
using MassTransitConsumer.Messages.Order;
using MassTransitProducer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using OrderApplication;
using OrderApplication.Extentions;
public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure( this WebApplicationBuilder  webApplicationBuilder)
    {
        webApplicationBuilder.AddMediatR();
        webApplicationBuilder.AddHealthCheck();
        webApplicationBuilder.Services.AddCustomMassTransitProducer<OrderMessage>( "Development");//,webApplicationBuilder.Environment

        webApplicationBuilder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        webApplicationBuilder.Services.AddControllers();
        webApplicationBuilder.Services.AddEndpointsApiExplorer();
        webApplicationBuilder.AddAuthoriz();
        webApplicationBuilder.Services.AddTransient<LoggingService>();
        return webApplicationBuilder;
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