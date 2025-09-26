namespace Birthsys.Identity.Domain.Aggregates.Users.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
        Task<bool> ExistsByEmailAsync(UserEmail email, CancellationToken cancellationToken = default);
        Task<User?> FindByIdAsync(UserId id, CancellationToken cancellationToken = default);
        Task<User?> FindByEmailAsync(UserEmail email, CancellationToken cancellationToken = default);
    }
}