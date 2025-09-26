using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;
using Birthsys.Identity.Domain.Aggregates.Users.Interfaces;

namespace Birthsys.Identity.Application.Queries.Users.FindById
{
    internal sealed class UserFindByIdQueryHandler(
        IUserRepository userRepository
    ): IQueryHandler<UserFindByIdQuery, User?>
    {

        public async Task<Result<User?>> Handle(UserFindByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.FindByIdAsync(request.Id, cancellationToken);

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