using Logging;
using MassTransitHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InventoryApplication.Extentions;
using InventoryAppliction.Consumers;
using InventoryDomain.Interfaces;
using InventoryInfrastructure;
using InventoryInfrastructure.UnitOfWork;
using SharedMessages;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddDbContext<InventoryContext>(options =>
        options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("InventoryConnection")));
        webApplicationBuilder.Services.AddScoped<IInventoryContext, InventoryContext>();

        webApplicationBuilder.Services.AddTransient<LoggingService>();
        webApplicationBuilder.Services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        webApplicationBuilder.AddMediatR();
        webApplicationBuilder.AddHealthCheck();

        webApplicationBuilder.Services.AddMassTransit<GenericConsumer>();

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