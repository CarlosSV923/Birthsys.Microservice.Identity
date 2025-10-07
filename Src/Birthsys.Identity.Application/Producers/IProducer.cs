using Birthsys.Identity.Domain.Abstractions;

namespace Birthsys.Identity.Application.Producers
{
    public interface IProducer<TMessage>
    {
        Task<Result> PublishAsync(TMessage message, Dictionary<string, object>? headers = null, CancellationToken cancellationToken = default);
        Task<Result> PublishBulkAsync(IReadOnlyList<TMessage> messages, Dictionary<string, object>? headers = null,CancellationToken cancellationToken = default);
    }
}