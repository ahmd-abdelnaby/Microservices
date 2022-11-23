using HealthChecks.System;
using HealthChecks.UI.Client;
using Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProductAppliction;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(LoggingConfigurtion.ConfigureLogger);
builder.Services.AddTransient<LoggingService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOrderApplication();

builder.Services.AddHealthChecks()
        .AddDiskStorageHealthCheck(delegate (DiskStorageOptions diskStorageOptions)
        {
            diskStorageOptions.AddDrive(@"C:\", 500000000000000000);
        }, name: "My Drive", HealthStatus.Unhealthy);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
