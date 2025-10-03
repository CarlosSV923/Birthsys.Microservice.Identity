using System.Net.Mime;
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
            });
        }
    }
}