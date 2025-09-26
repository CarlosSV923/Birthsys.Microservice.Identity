using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;
using Birthsys.Identity.Domain.Aggregates.Users.Interfaces;

namespace Birthsys.Identity.Application.Queries.Users.ExistsByEmail
{
    internal sealed class UserExistsByEmailQueryHandler(
        IUserRepository userRepository
    ) : IQueryHandler<UserExistsByEmailQuery, bool>
    {
        public Task<Result<bool>> Handle(UserExistsByEmailQuery request, CancellationToken cancellationToken)
        {
            return userRepository.ExistsByEmailAsync(request.Email, cancellationToken).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine(task.Exception);
                    return Result.Failure<bool>(UserErrors.ErrorCheckingUserEmail);
                }

                return task.Result;
            }, cancellationToken);
        }
    }
}