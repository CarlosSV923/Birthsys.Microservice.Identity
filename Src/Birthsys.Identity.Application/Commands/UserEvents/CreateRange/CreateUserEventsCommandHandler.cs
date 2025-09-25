using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.UserEvents.Interfaces;
namespace Birthsys.Identity.Application.Commands.UserEvents.CreateRange
{

    internal sealed class CreateUserEventsCommandHandler(
        IUserEventRepository userEventRepository,
        IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateUserEventsCommand>
    {
        public async Task<Result> Handle(CreateUserEventsCommand request, CancellationToken cancellationToken)
        {
            var userEvents = request.UserEvents;

            try
            {
                userEventRepository.AddRange(userEvents);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                Console.WriteLine(ex);
                return Result.Failure(UserEventErrors.ErrorCreatingUserEvent);
            }
        }
    }
}