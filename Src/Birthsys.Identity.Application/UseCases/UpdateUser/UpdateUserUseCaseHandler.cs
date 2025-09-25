using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Application.Commands.Users.Update;
using Birthsys.Identity.Application.Events;
using Birthsys.Identity.Application.Queries.Users.ExistsByEmail;
using Birthsys.Identity.Application.Queries.Users.FindById;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using Birthsys.Identity.Domain.Aggregates.Users.Inputs;
using Birthsys.Identity.Domain.Aggregates.Users.Interfaces;
using MediatR;

namespace Birthsys.Identity.Application.UseCases.UpdateUser
{
    internal sealed class UpdateUserUseCaseHandler(
        IMediator mediator,
        IEventsPublisherProvider eventsPublisherProvider
        ) : IUseCaseHandler<UpdateUserUseCaseInput, UpdateUserUseCaseOutput>
    {
        private readonly IMediator mediator = mediator;
        private readonly IEventsPublisherProvider eventsPublisherProvider = eventsPublisherProvider;

        public async Task<Result<UpdateUserUseCaseOutput>> Handle(UpdateUserUseCaseInput request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var userId))
            {
                return Result.Failure<UpdateUserUseCaseOutput>(UserErrors.InvalidUserId);
            }

            var userResult = await mediator.Send(new UserFindByIdQuery(UserId.FromGuid(userId)), cancellationToken);
            if (!userResult.IsSuccess)
            {
                return Result.Failure<UpdateUserUseCaseOutput>(userResult.Error);
            }

            var user = userResult.Value;
            if (user!.Email!.Value != request.Email)
            {
                var emailExistsResult = await mediator.Send(new UserExistsByEmailQuery(new UserEmail(request.Email!)), cancellationToken);
                if (!emailExistsResult.IsSuccess)
                {
                    return Result.Failure<UpdateUserUseCaseOutput>(emailExistsResult.Error);
                }
                if (emailExistsResult.Value)
                {
                    return Result.Failure<UpdateUserUseCaseOutput>(UserErrors.EmailAlreadyExists);
                }
            }

            user
                .UpdateEmail(request.Email)
                .UpdateName(request.Name)
                .UpdateLastName(request.LastName)
                .UpdateDateOfBirth(request.DateOfBirth);

            var userEvent = UserEvent.CreateUpdateUserEvent(user.Id!);
            var updateResult = await mediator.Send(new UpdateUserCommand(user, [userEvent]), cancellationToken);
            if (!updateResult.IsSuccess)
            {
                return Result.Failure<UpdateUserUseCaseOutput>(updateResult.Error);
            }
            await eventsPublisherProvider.PublishEventsAsync(user, [userEvent], cancellationToken);
            return new UpdateUserUseCaseOutput(user.Id!.Value);
        }
    }
}