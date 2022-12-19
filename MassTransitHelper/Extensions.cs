using ConfigurationExtensions;
using MassTransit;
using MassTransitHelper.Saga;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace MassTransitHelper
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
                var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).ToList();
                var ConsumerTypes = allTypes.Where(x => x.IsAssignableTo(typeof(TConsumer))).ToList();

                var rabbitMqOptions = services.GetOptions<RabbitMqOptions>("RabbitMq");
                var host = IsRunningInContainer ? "rabbitmq" : rabbitMqOptions.HostName;

                if (rabbitMqOptions.ExchangeType.Equals("Direct")
                       || rabbitMqOptions.ExchangeType.Equals("Topic"))
                {
                    services.AddMassTransit(config =>
                    {

                        foreach (var consumer in ConsumerTypes)
                        {
                            if (!consumer.Name.Equals(typeof(TConsumer).Name))
                                config.AddConsumers((Type)consumer);
                        }
                        config.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(host, h =>
                            {
                                h.Username(rabbitMqOptions.UserName);
                                h.Password(rabbitMqOptions.Password);
                            });

                            foreach (var consumer in ConsumerTypes)
                            {
                                if (!consumer.Name.Equals(typeof(TConsumer).Name))
                                {

                                    cfg.ReceiveEndpoint(rabbitMqOptions.QueueName + consumer.Name, re =>
                                          {
                                        re.ConfigureConsumeTopology = false;

                                        re.ConfigureConsumer(context, consumer);


                                        re.Bind(rabbitMqOptions.ExchangeName, e =>
                                        {
                                            e.RoutingKey = consumer.Name;
                                            e.ExchangeType = rabbitMqOptions.ExchangeType.Equals("Direct") ? ExchangeType.Direct : ExchangeType.Topic;
                                        });
                                    });
                                }
                            }

                        });
                    });
                }

                else
                {
                    services.AddMassTransit(config =>
                    {
                        config.AddSagaStateMachine<OrderStateMachine, OrderState>()
                                    .InMemoryRepository();
                            //.EntityFrameworkRepository(r =>
                            //{
                            //    r.ConcurrencyMode = ConcurrencyMode.Pessimistic;

                            //    r.AddDbContext<DbContext, StateMachineDbContext>((provider, optionsBuilder) =>
                            //    {
                            //        optionsBuilder.UseSqlServer("connection string");
                            //    });

                            //});




                            foreach (var consumer in ConsumerTypes)
                        {
                            if (!consumer.Name.Equals(typeof(TConsumer).Name))
                                config.AddConsumers((Type)consumer);
                        }

                        config.UsingRabbitMq((ctx, cfg) =>
                            {
                                cfg.Host(host, h =>
                                {
                                    h.Username(rabbitMqOptions.UserName);
                                    h.Password(rabbitMqOptions.Password);
                                });
                                if (ConsumerTypes.Any(x => x.Name != (typeof(TConsumer).Name)))
                                {
                                    cfg.ConfigureEndpoints(ctx);
                                }
                            });

                    });
                }


            }

            return services;

        }


        public static IServiceCollection AddMassTransit<TConsumer>(this IServiceCollection services)
        {

            var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).ToList();
            var ConsumerTypes = allTypes.Where(x => x.IsAssignableTo(typeof(TConsumer))).ToList();

            var rabbitMqOptions = services.GetOptions<RabbitMqOptions>("RabbitMq");

            services.AddMassTransit(config =>
            {
                config.AddSagaStateMachine<OrderStateMachine, OrderState>()
                            .InMemoryRepository();

                foreach (var consumer in ConsumerTypes)
                {
                    if (!consumer.Name.Equals(typeof(TConsumer).Name))
                        config.AddConsumers((Type)consumer);
                }

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", h =>
                    {
                        h.Username(rabbitMqOptions.UserName);
                        h.Password(rabbitMqOptions.Password);
                    });
                    if (ConsumerTypes.Any(x => x.Name != (typeof(TConsumer).Name)))
                    {
                        cfg.ConfigureEndpoints(ctx);
                    }
                });

            });
            return services;

        }
    }
}
