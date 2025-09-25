using Birthsys.Identity.Domain.Aggregates.Users;
using Birthsys.Identity.Domain.Aggregates.Users.Interfaces;
using Birthsys.Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Birthsys.Identity.Infrastructure.Repositories
{
    internal sealed class UserRepository(Context dbContext) : RepositoryBase<User, UserId>(dbContext), IUserRepository
    {
        public Task<bool> ExistsByEmailAsync(UserEmail email, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<User>().AnyAsync(u => u.Email == email, cancellationToken);
        }

        public Task<User?> FindByEmailAsync(UserEmail email, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
