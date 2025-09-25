using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Application.Commands.Users.Update;
using Birthsys.Identity.Application.Events;
using Birthsys.Identity.Application.Queries.Users.FindById;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;

namespace Birthsys.Identity.Application.UseCases.DeleteUser
{
    internal sealed class DeleteUserUseCaseHandler(
        IMediator mediator,
        IEventsPublisherProvider eventsPublisherProvider
        ) : IUseCaseHandler<DeleteUserUseCaseInput, DeleteUserUseCaseOutput>
    {
        public async Task<Result<DeleteUserUseCaseOutput>> Handle(DeleteUserUseCaseInput request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var userId))
            {
                return Result.Failure<DeleteUserUseCaseOutput>(UserErrors.InvalidUserId);
            }

            var userResult = await mediator.Send(new UserFindByIdQuery(UserId.FromGuid(userId)), cancellationToken);
            if (!userResult.IsSuccess)
            {
                return Result.Failure<DeleteUserUseCaseOutput>(userResult.Error);
            }

            var user = userResult.Value!;
            var userDeletedEvent = UserEvent.CreateUserDeletedEvent(user.Id!);
            user.Deactivate();
            var deleteResult = await mediator.Send(new UpdateUserCommand(user, [userDeletedEvent]), cancellationToken);
            if (!deleteResult.IsSuccess)
            {
                return Result.Failure<DeleteUserUseCaseOutput>(deleteResult.Error);
            }

            await eventsPublisherProvider.PublishEventsAsync(user, [userDeletedEvent], cancellationToken);

            return new DeleteUserUseCaseOutput(user.Id!.Value);
        }
    }
}