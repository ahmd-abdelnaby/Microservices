using Logging;
using MassTransitHelper;
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
using OrderApplication.Settings;
using SharedMessages;
using System.Configuration;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure( this WebApplicationBuilder  webApplicationBuilder)
    {
        webApplicationBuilder.AddMediatR();
        webApplicationBuilder.AddHealthCheck();
        webApplicationBuilder.Services.AddMassTransit<GenericConsumer>();
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
        webApplicationBuilder.Services.Configure<InventorySettings>(webApplicationBuilder.Configuration.GetSection("InventorySettings"));

        return webApplicationBuilder;
    }
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        app.UseCors("CorsPolicy");
        app.UseHealthCheck();
        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseAuthoriz();
      
        return app;
    }
}