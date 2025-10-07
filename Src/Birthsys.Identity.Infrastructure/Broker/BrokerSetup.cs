using System.Net.Mime;
using Birthsys.Identity.Application.Commands.UserEvents.UpdateRange;
using Birthsys.Identity.Application.Consumers.UpdateUserEvent;
using Birthsys.Identity.Application.Producers.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlimMessageBus.Host;
using SlimMessageBus.Host.RabbitMQ;
using SlimMessageBus.Host.Serialization.SystemTextJson;

namespace Birthsys.Identity.Infrastructure.Broker
{
    public static class BrokerSetup
    {
        public static void AddBroker(this IServiceCollection services, IConfiguration configuration)
        {
            var rbmqOptions = configuration.GetSection(RabbitMQOptions.SectionName).Get<RabbitMQOptions>()!;
            services.AddSlimMessageBus(options =>
            {
                options.WithProviderRabbitMQ(configure =>
                    {
                        configure.ConnectionFactory.HostName = rbmqOptions.Host;
                        configure.ConnectionFactory.Port = rbmqOptions.Port;
                        configure.ConnectionFactory.UserName = rbmqOptions.Username;
                        configure.ConnectionFactory.Password = rbmqOptions.Password;
                        configure.ConnectionFactory.VirtualHost = rbmqOptions.VirtualHost;

                        configure.UseExchangeDefaults(durable: true);
                        configure.UseQueueDefaults(durable: true);

                        configure.UseMessagePropertiesModifier((m, p) =>
                        {
                            p.ContentType = MediaTypeNames.Application.Json;
                        });
                    });
                options.AddJsonSerializer();

                options.Consume<UpdateUserEventMessage>(x => x
                    .Queue("update_userevent_queue")
                    .ExchangeBinding("userevent_exchange", "userevent_updated")
                    .WithConsumer<UpdateUserEventConsumer>()
                );

                options.Produce<UpdateUserProducerEvent>(x => x
                    .Exchange("user_exchange", exchangeType: ExchangeType.Direct)
                    .RoutingKeyProvider((m, p) => "user_updated")
                );

                options.Produce<CreateUserProducerEvent>(x => x
                    .Exchange("user_exchange", exchangeType: ExchangeType.Direct)
                    .RoutingKeyProvider((m, p) => "user_created")
                );

                options.Produce<DeleteUserProducerEvent>(x => x
                    .Exchange("user_exchange", exchangeType: ExchangeType.Direct)
                    .RoutingKeyProvider((m, p) => "user_deleted")
                );

                options.Produce<ChangeUserPasswordProducerEvent>(x => x
                    .Exchange("user_exchange", exchangeType: ExchangeType.Direct)
                    .RoutingKeyProvider((m, p) => "user_password_changed")
                );

                options.Produce<LoginUserProducerEvent>(x => x
                    .Exchange("user_exchange", exchangeType: ExchangeType.Direct)
                    .RoutingKeyProvider((m, p) => "user_logged_in")
                );
            });
        }
    }
}