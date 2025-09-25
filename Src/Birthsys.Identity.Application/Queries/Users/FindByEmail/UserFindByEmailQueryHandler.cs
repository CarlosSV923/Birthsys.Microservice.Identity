using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;
using Birthsys.Identity.Domain.Aggregates.Users.Interfaces;

namespace Birthsys.Identity.Application.Queries.Users.FindByEmail
{
    internal sealed class UserFindByEmailQueryHandler(
        IUserRepository userRepository
    ): IQueryHandler<UserFindByEmailQuery, User?>
    {

        public async Task<Result<User?>> Handle(UserFindByEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.FindByEmailAsync(request.Email, cancellationToken);

                if (user is null)
                {
                    return Result.Failure<User?>(UserErrors.UserNotFound);
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Result.Failure<User?>(UserErrors.ErrorFindingUser);
            }
        }
    }
}