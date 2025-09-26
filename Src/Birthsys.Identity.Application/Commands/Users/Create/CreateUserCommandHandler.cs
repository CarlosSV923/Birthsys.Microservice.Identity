using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents.Interfaces;
using Birthsys.Identity.Domain.Aggregates.Users;
using Birthsys.Identity.Domain.Aggregates.Users.Interfaces;

namespace Birthsys.Identity.Application.Commands.Users.Create
{
    internal sealed class CreateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IUserEventRepository userEventRepository
    ) : ICommandHandler<CreateUserCommand>
    {
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.User;
            var userEvents = request.Events;

            try
            {
                userRepository.Add(user);
                userEventRepository.AddRange(userEvents);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                Console.WriteLine(ex);
                return Result.Failure(UserErrors.ErrorCreatingUser);
            }
        }
    }
}