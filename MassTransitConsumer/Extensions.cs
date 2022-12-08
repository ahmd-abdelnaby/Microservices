using ConfigurationExtensions;
using MassTransit;
using MassTransit.Internals;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.Logging.OperationName;

namespace MassTransitConsumer
{
    public static class Extensions
    {
        private static bool? _isRunningInContainer;

        private static bool IsRunningInContainer => _isRunningInContainer ??=
            bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) &&
            inContainer;

        public static IServiceCollection AddCustomMassTransitConsumer<TConsumer,TMessage>( this IServiceCollection services, string env)
             where TConsumer : class,  IConsumer where TMessage : class
        {

/*
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                        .Where(x => x.IsAssignableTo(typeof(TConsumer))
                                    && !x.IsInterface
                                    && !x.IsAbstract
                                    *//*&& !x.IsGenericType*//*).FirstOrDefault();

           
                var consumers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                           .Where(x => x.IsAssignableTo(typeof(IConsumer<>).MakeGenericType(typeof(TConsumer)))).fi();*/
            

            /*var type =  Assembly.GetAssembly(typeof(TConsumer)).GetTypes()
           .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(TConsumer))).FirstOrDefault().GetType();
*/
 /*           IEnumerable<asd> exporters = typeof(asd)
    .Assembly.GetTypes()
    .Where(t => t.IsSubclassOf(typeof(asd)) && !t.IsAbstract)
    .Select(t => (TConsumer)Activator.CreateInstance(asd)).ToList();*/

            // if (!env.HostingEnvironment.IsProduction())
            if (env.Equals("Development"))
            {
              /*  var x=clazz.GetType();
                var xxx =clazz.GetTypeInfo();
                var xx=clazz.GetTypeName();*/
                
                var rabbitMqOptions = services.GetOptions<RabbitMqOptions>("RabbitMq");
                var host = IsRunningInContainer ? "rabbitmq" : rabbitMqOptions.HostName;

                if (rabbitMqOptions.ExchangeType.Equals("Direct")
                       || rabbitMqOptions.ExchangeType.Equals("Topic"))
                {
                    services.AddMassTransit(config =>
                    {
                        //config.AddSagaStateMachine<OrderStateMachine, OrderState>()
                        //            .InMemoryRepository();
                        config.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(host, h =>
                            {
                                h.Username(rabbitMqOptions.UserName);
                                h.Password(rabbitMqOptions.Password);
                            });

                            cfg.ReceiveEndpoint(rabbitMqOptions.QueueName, re =>     //Queue Name
                            {


                              //  re.Consumer<TConsumer>();

                                re.ConfigureConsumeTopology = false;
                                // re.SetQuorumQueue();
                                if (rabbitMqOptions.IsLasyQueue)
                                {
                                    re.SetQueueArgument("declare", "lazy");
                                }

                                re.Bind(rabbitMqOptions.ExchangeName, e =>
                                {
                                    e.RoutingKey = rabbitMqOptions.RouteKey;
                                    e.ExchangeType = rabbitMqOptions.ExchangeType.Equals("Direct") ? ExchangeType.Direct : ExchangeType.Topic;
                                });

                            });


                        });
                    });
                }

                else
                {
                    services.AddMassTransit(config =>
                    {
                     
                         config.AddConsumer<TConsumer>();

                        config.UsingRabbitMq((ctx, cfg) =>
                        {
                            cfg.Host(host, h =>
                            {
                                h.Username(rabbitMqOptions.UserName);
                                h.Password(rabbitMqOptions.Password);
                            });
                            cfg.ReceiveEndpoint(rabbitMqOptions.QueueName, c =>
                            {
                              c.ConfigureConsumer<TConsumer>(ctx);

                            });
                        });
                    });
                }
                    
            }

            return services;
        }

       

        
    }
}
