using Logging;
using PaymentGrpcService.Services;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructure();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Host.UseSerilog(LoggingConfigurtion.ConfigureLogger);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseInfrastructure();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<PaymentService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
