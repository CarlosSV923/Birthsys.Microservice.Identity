using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.UserEvents.Interfaces;
using Birthsys.Identity.Domain.Aggregates.Users;
using Birthsys.Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Birthsys.Identity.Infrastructure.Repositories
{
    internal sealed class UserEventRepository(Context dbContext) : RepositoryBase<UserEvent, UserEventId>(dbContext), IUserEventRepository
    {
        public void AddRange(IReadOnlyList<UserEvent> userEvents)
        {
            _dbContext.Set<UserEvent>().AddRange(userEvents);
        }

        public async Task<IReadOnlyList<UserEvent>> FindByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UserEvent>()
                .Where(ue => ue.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public void UpdateRange(IReadOnlyList<UserEvent> userEvents)
        {
            _dbContext.Set<UserEvent>().UpdateRange(userEvents);
        }
    }
}