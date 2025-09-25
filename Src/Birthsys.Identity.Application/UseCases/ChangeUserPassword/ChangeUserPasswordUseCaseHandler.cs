using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Application.Commands.Users.Update;
using Birthsys.Identity.Application.Events;
using Birthsys.Identity.Application.Queries.Users.FindById;
using Birthsys.Identity.Application.Services.PasswordHasher;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;

namespace Birthsys.Identity.Application.UseCases.ChangeUserPassword
{
    internal sealed class ChangeUserPasswordUseCaseHandler(
        IMediator mediator,
        IEventsPublisherProvider eventsPublisherProvider,
        IPasswordHasherProvider passwordHasher
    ) : IUseCaseHandler<ChangeUserPasswordUseCaseInput, ChangeUserPasswordUseCaseOutput>
    {
        public async Task<Result<ChangeUserPasswordUseCaseOutput>> Handle(ChangeUserPasswordUseCaseInput request, CancellationToken cancellationToken)
        {

            var userResult = await mediator.Send(new UserFindByIdQuery(UserId.FromString(request.Id)), cancellationToken);
            if (!userResult.IsSuccess)
            {
                return Result.Failure<ChangeUserPasswordUseCaseOutput>(userResult.Error);
            }

            var user = userResult.Value!;
            if (!passwordHasher.VerifyPassword(request.CurrentPassword, user.PasswordHash!.Value, user.PasswordSalt!.Value))
            {
                return Result.Failure<ChangeUserPasswordUseCaseOutput>(UserErrors.InvalidCurrentPassword);
            }

            var newPasswordHash = passwordHasher.HashPassword(request.NewPassword, out var newPasswordSalt);
            user.UpdatePassword(newPasswordHash, newPasswordSalt);

            var changePasswordSuccessEvent = UserEvent.CreateChangeUserPasswordEvent(user.Id!);
            var updateResult = await mediator.Send(new UpdateUserCommand(user, [changePasswordSuccessEvent]), cancellationToken);
            if (!updateResult.IsSuccess)
            {
                return Result.Failure<ChangeUserPasswordUseCaseOutput>(updateResult.Error);
            }
            await eventsPublisherProvider.PublishEventsAsync(user, [changePasswordSuccessEvent], cancellationToken);

            return new ChangeUserPasswordUseCaseOutput(user.Id!.Value.ToString());
        }
    }
}