using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var authenticationProviderKey = "IdentityApiKey";

            new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddOcelot("OcelotConfigrations", hostingContext.HostingEnvironment)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices(s => {
                s.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });
                s.AddAuthentication()
                     .AddJwtBearer(authenticationProviderKey, x =>
                     {
                         x.Authority = "https://localhost:5000"; 
                         x.RequireHttpsMetadata = false;
                         x.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateAudience = false
                         };
                     });
                s.AddOcelot();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                //add your logging
            })
            .UseIISIntegration()
            .Configure(app =>
            {
                app.UseCors("CorsPolicy");
                app.UseAuthentication();
                app.UseOcelot().Wait();
            })
            .Build()
            .Run();
        }
    }
}