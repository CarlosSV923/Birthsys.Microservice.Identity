using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.UserEvents.Interfaces;

namespace Birthsys.Identity.Application.Commands.UserEvents.UpdateRange
{
    internal sealed class UpdateUserEventsCommandHandler(
        IUserEventRepository userEventRepository,
        IUnitOfWork unitOfWork
    ) : ICommandHandler<UpdateUserEventsCommand>
    {
        public async Task<Result> Handle(UpdateUserEventsCommand request, CancellationToken cancellationToken)
        {
            var userEvents = request.UserEvents;

            try
            {
                userEventRepository.UpdateRange(userEvents);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                Console.WriteLine(ex);
                return Result.Failure(UserEventErrors.ErrorUpdatingUserEvent);
            }
        }
    }
}