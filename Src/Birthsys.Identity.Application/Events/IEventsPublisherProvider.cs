using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Application.Events
{
    public interface IEventsPublisherProvider
    {
        Task CreateAndPublishEventsAsync(User user, IReadOnlyList<UserEvent> events, CancellationToken cancellationToken = default);

        Task PublishEventsAsync(User user, IReadOnlyList<UserEvent> events, CancellationToken cancellationToken = default);
    }
}
