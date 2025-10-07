namespace Birthsys.Identity.Infrastructure.Broker
{
    public class BrokerOptions
    {
        public const string SectionName = "Broker";

        // RabbitMQ
        public RabbitMQOptions RabbitMQ { get; set; } = new();
    }

    public class RabbitMQOptions
    {
        public const string SectionName = "Broker:RabbitMQ";
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 5672;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string VirtualHost { get; set; } = string.Empty;
    }

    
}