using ConfigurationExtensions;
using MassTransit;
using MassTransitConsumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Reflection;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MassTransitProducer
{
    public static class Extensions
    {
        private static bool? _isRunningInContainer;

        private static bool IsRunningInContainer => _isRunningInContainer ??=
            bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) &&
        inContainer;

        public static IServiceCollection AddCustomMassTransitProducer<TMessage>(this IServiceCollection services, string env) where TMessage : class//HostBuilderContext env  //Type MessageType
        {
           
           // var message= MessageType.;  
           /* var x = assembly.GetTypes().FirstOrDefault().GetTypeInfo();
            var y = assembly.GetExecutingAssembly().FullName;*/
            // if (!env.HostingEnvironment.IsProduction())
            if (env.Equals("Development"))
            {

                services.AddMassTransit(config =>
                {
                    config.UsingRabbitMq((context, cfg) =>
                    {

                        var rabbitMqOptions = services.GetOptions<RabbitMqOptions>("RabbitMq");
                        var host = IsRunningInContainer ? "rabbitmq" : rabbitMqOptions.HostName;

                        cfg.Host(host, h =>
                        {
                            h.Username(rabbitMqOptions.UserName);
                            h.Password(rabbitMqOptions.Password);
                        });

                        if (rabbitMqOptions.ExchangeType.Equals("Direct")
                       || rabbitMqOptions.ExchangeType.Equals("Topic"))
                        {
                            cfg.Message<TMessage>
                          (e => e.SetEntityName(rabbitMqOptions.ExchangeName)); // name of the primary exchange

                            cfg.Publish<TMessage>
                            (e => e.ExchangeType = rabbitMqOptions.ExchangeType.Equals("Direct") ? ExchangeType.Direct : ExchangeType.Topic); // primary exchange type

                            cfg.Send<TMessage>(e =>
                            {
                                e.UseRoutingKeyFormatter(context =>
                                {
                                    return rabbitMqOptions.RouteKey;    // route key
                                });
                            });

                        }
                    });
                });
            }
        

            return services;
        }
    }
}
