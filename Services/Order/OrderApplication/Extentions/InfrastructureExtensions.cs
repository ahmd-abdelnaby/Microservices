using Logging;
using MassTransitConsumer;
using MassTransitProducer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using OrderApplication;
using OrderApplication.AutoMapper;
using OrderApplication.Context;
using OrderApplication.Extentions;
using SharedMessages;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure( this WebApplicationBuilder  webApplicationBuilder)
    {
        //webApplicationBuilder.AddMediatR();
        //webApplicationBuilder.AddHealthCheck();
        webApplicationBuilder.Services.AddCustomMassTransitProducer<InventoryQuantities>( "Development");

        webApplicationBuilder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        webApplicationBuilder.Services.AddDbContext<OrderDBContext>(options =>
        options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("connectionString")));

        webApplicationBuilder.Services.AddControllers();
        webApplicationBuilder.Services.AddEndpointsApiExplorer();
        webApplicationBuilder.AddAuthoriz();
        webApplicationBuilder.Services.AddTransient<LoggingService>();

        webApplicationBuilder.Services.AddAutoMapper(typeof(AutoMapperConfig));

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