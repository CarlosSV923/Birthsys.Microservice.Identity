using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Domain.Aggregates.UserEvents.Interfaces
{
    public interface IUserEventRepository
    {
        void Add(UserEvent userEvent);
        void AddRange(IReadOnlyList<UserEvent> userEvents);
        void Update(UserEvent userEvent);
        void UpdateRange(IReadOnlyList<UserEvent> userEvents);
        Task<UserEvent?> FindByIdAsync(UserEventId id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<UserEvent>> FindByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
    }
}