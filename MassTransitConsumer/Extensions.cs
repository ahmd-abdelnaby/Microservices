using ConfigurationExtensions;
using MassTransit;
using MassTransit.Internals;
using MassTransitConsumer.Saga;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MassTransit.Logging.OperationName;
using static MassTransit.Monitoring.Performance.BuiltInCounters;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace MassTransitConsumer
{
    public static class Extensions
    {
        

        private static bool? _isRunningInContainer;

        private static bool IsRunningInContainer => _isRunningInContainer ??=
            bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) &&
            inContainer;

        public static IServiceCollection AddCustomMassTransitConsumer<TConsumer, TMessage>(this IServiceCollection services, string env)
             /*where TConsumer : class, IConsumer*/ where TMessage : class
        {
           
                // if (!env.HostingEnvironment.IsProduction())
                if (env.Equals("Development"))
                {
                  
                    var rabbitMqOptions = services.GetOptions<RabbitMqOptions>("RabbitMq");
                    var host = IsRunningInContainer ? "rabbitmq" : rabbitMqOptions.HostName;

                    if (rabbitMqOptions.ExchangeType.Equals("Direct")
                           || rabbitMqOptions.ExchangeType.Equals("Topic"))
                    {
                        services.AddMassTransit(config =>
                        {

                            // config.AddConsumer<TConsumer>();
                            config.AddSagaStateMachine<OrderStateMachine, OrderState>()
                                                                   .InMemoryRepository();

                            config.UsingRabbitMq((context, cfg) =>
                            {
                                cfg.Host(host, h =>
                                {
                                    h.Username(rabbitMqOptions.UserName);
                                    h.Password(rabbitMqOptions.Password);
                                });
                                cfg.ConfigureEndpoints(context);
                                cfg.ReceiveEndpoint(rabbitMqOptions.QueueName, re =>     //Queue Name
                                {

                                    re.ConfigureConsumeTopology = false;
                                    // re.SetQuorumQueue();
                                    if (rabbitMqOptions.IsLasyQueue)
                                    {
                                        re.SetQueueArgument("declare", "lazy");
                                    }

                                    re.Bind(rabbitMqOptions.ExchangeName, e =>
                                    {
                                        e.RoutingKey = rabbitMqOptions.RouteKey;
                                        e.ExchangeType = rabbitMqOptions.ExchangeType.Equals("Direct") ? ExchangeType.Fanout : ExchangeType.Fanout;
                                    });

                                });


                            });
                        });
                    }

                    else
                    {
                        services.AddMassTransit(config =>
                        {
                            config.AddSagaStateMachine<OrderStateMachine, OrderState>()
                                       .InMemoryRepository();


                            var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).ToList();

                            var ConsumerTypes = allTypes.Where(x => x.IsAssignableTo(typeof(TConsumer))).ToList();

                            foreach (var consumer in ConsumerTypes)
                            {
                                if(!consumer.Name.Equals(typeof(TConsumer).Name))
                                config.AddConsumers((Type)consumer);
                            }

                            config.UsingRabbitMq((ctx, cfg) =>
                                {
                                    cfg.Host(host, h =>
                                    {
                                        h.Username(rabbitMqOptions.UserName);
                                        h.Password(rabbitMqOptions.Password);
                                    });
                                    cfg.ConfigureEndpoints(ctx);
                                    if (ConsumerTypes.Any(x=>x.Name.Equals(typeof(TConsumer).Name)))
                                    {
                                        // rabbitSettings.QueueName => service-b
                                        //cfg.ReceiveEndpoint(rabbitMqOptions.QueueName, e =>
                                        //{
                                        //    // e.UseConsumeFilter(typeof(InboxFilter<>), context);
                                        //    e.ConfigureConsumers(ctx);
                                        //});
                                    }
                                });
                            
                        });
                    }
           

            }

                return services;
          
            }
        
    }
}
