using Birthsys.Identity.Domain.Abstractions;

namespace Birthsys.Identity.Application.Producers
{
    public interface IProducer<TMessage>
    {
        Task<Result> PublishAsync(TMessage message, CancellationToken cancellationToken = default);
    }
}