using HealthChecks.System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddDiskStorageHealthCheck(delegate (DiskStorageOptions diskStorageOptions)
    {
        diskStorageOptions.AddDrive(@"C:\", 5000);
    }, name: "My Drive", HealthStatus.Unhealthy)
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddHealthChecksUI().AddInMemoryStorage();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).UseHealthChecksUI( options=>
{
    options.UIPath = "/hc-ui";
    options.AddCustomStylesheet("./Customization/custom.css");
});

app.UseRouting();
app.UseHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}); 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
