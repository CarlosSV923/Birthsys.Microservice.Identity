using Birthsys.Identity.Domain.Abstractions;
using SlimMessageBus;

namespace Birthsys.Identity.Application.Producers
{
    public class Producer<TMessage>(
        IMessageBus messageBus
    ) : IProducer<TMessage>
    {
        public async Task<Result> PublishAsync(TMessage message, Dictionary<string, object>? headers = null, CancellationToken cancellationToken = default)
        {
            try
            {
                await messageBus.Publish(message, headers: headers, cancellationToken: cancellationToken);
                Console.WriteLine($"Message published successfully: {typeof(TMessage).Name}");
                return Result.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to publish message: {typeof(TMessage).Name}", ex);
                return Result.Failure(Error.Build(500, "PublishFailed", "Failed to publish message"));
            }
        }

        public async Task<Result> PublishBulkAsync(IReadOnlyList<TMessage> messages, Dictionary<string, object>? headers = null, CancellationToken cancellationToken = default)
        {
            try
            {
                await messageBus.Publish(messages, headers: headers, cancellationToken: cancellationToken);
                Console.WriteLine($"Messages published successfully: {typeof(TMessage).Name}");
                return Result.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to publish messages: {typeof(TMessage).Name}", ex);
                return Result.Failure(Error.Build(500, "PublishFailed", "Failed to publish messages"));
            }
        }
    }
}