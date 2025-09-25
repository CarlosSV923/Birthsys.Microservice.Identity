using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Application.Commands.UserEvents.CreateRange;
using Birthsys.Identity.Application.Events;
using Birthsys.Identity.Application.Queries.Users.FindByEmail;
using Birthsys.Identity.Application.Services.Jwt;
using Birthsys.Identity.Application.Services.PasswordHasher;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using Birthsys.Identity.Domain.Aggregates.Users.Interfaces;
using MediatR;

namespace Birthsys.Identity.Application.UseCases.LoginUser
{
    internal sealed class LoginUserUseCaseHandler(
        IMediator _mediator,
        IPasswordHasherProvider _passwordHasher,
        IJwtProvider _jwtProvider,
        IEventsPublisherProvider _eventsPublisherProvider
    ) : IUseCaseHandler<LoginUserUseCaseInput, LoginUserUseCaseOutput>
    {

        public async Task<Result<LoginUserUseCaseOutput>> Handle(LoginUserUseCaseInput request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new UserFindByEmailQuery(new UserEmail(request.Email)), cancellationToken);

            if (!userResult.IsSuccess)
            {
                return Result.Failure<LoginUserUseCaseOutput>(userResult.Error);
            }

            var user = userResult.Value!;
            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash!.Value, user.PasswordSalt!.Value))
            {
                var error = UserErrors.InvalidCredentials;
                var loginFailedEvent = UserEvent.CreateLoginFailedEvent(user.Id!, error);
                await _eventsPublisherProvider.CreateAndPublishEventsAsync(user, [loginFailedEvent], cancellationToken);

                return Result.Failure<LoginUserUseCaseOutput>(error);
            }

            var tokenResult = _jwtProvider.GenerateToken(user);
            if (!tokenResult.IsSuccess)
            {
                return Result.Failure<LoginUserUseCaseOutput>(tokenResult.Error);
            }

            var loginSuccessEvent = UserEvent.CreateLoginSuccessEvent(user.Id!);
            await _eventsPublisherProvider.CreateAndPublishEventsAsync(user, [loginSuccessEvent], cancellationToken);

            return new LoginUserUseCaseOutput(tokenResult.Value.Token, tokenResult.Value.Expiration);
        }
    }
}