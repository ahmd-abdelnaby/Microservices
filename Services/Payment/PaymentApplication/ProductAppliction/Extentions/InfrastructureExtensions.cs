using Logging;
using MassTransitConsumer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentApplication.Extentions;
using PaymentAppliction.Consumers;
using PaymentDomain.Interfaces;
using PaymentInfrastructure;
using PaymentInfrastructure.UnitOfWork;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddDbContext<PaymentContext>(options =>
        options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("PaymentConnection")));
        webApplicationBuilder.Services.AddScoped<IPaymentContext, PaymentContext>();

        webApplicationBuilder.Services.AddTransient<LoggingService>();
        webApplicationBuilder.Services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        webApplicationBuilder.AddMediatR();
        webApplicationBuilder.AddHealthCheck();



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