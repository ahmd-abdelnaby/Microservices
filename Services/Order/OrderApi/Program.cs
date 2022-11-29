using HealthChecks.UI.Client;
using Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(LoggingConfigurtion.ConfigureLogger);
builder.Services.AddInfrastructure();
builder.Services.AddMediatR();

builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMediatR();
app.UseInfrastructure();
app.Run();