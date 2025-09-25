using System.Net.NetworkInformation;
using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Application.Commands.Users.Create;
using Birthsys.Identity.Application.Events;
using Birthsys.Identity.Application.Queries.Users.ExistsByEmail;
using Birthsys.Identity.Application.Services.PasswordHasher;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using Birthsys.Identity.Domain.Aggregates.Users.Inputs;
using MediatR;

namespace Birthsys.Identity.Application.UseCases.RegisterUser
{
    internal sealed class RegisterUserUseCaseHandler(
        IMediator mediator,
        IPasswordHasherProvider passwordHasherProvider,
        IEventsPublisherProvider eventsPublisherProvider
    ) : IUseCaseHandler<RegisterUserUseCaseInput, RegisterUserUseCaseOutput>
    {
        public async Task<Result<RegisterUserUseCaseOutput>> Handle(RegisterUserUseCaseInput request, CancellationToken cancellationToken)
        {
            var userEmailExists = await mediator.Send(new UserExistsByEmailQuery(new UserEmail(request.UserEmail)), cancellationToken);
            if (!userEmailExists.IsSuccess)
            {
                return Result.Failure<RegisterUserUseCaseOutput>(userEmailExists.Error);
            }
            if (userEmailExists.Value)
            {
                return Result.Failure<RegisterUserUseCaseOutput>(UserErrors.EmailAlreadyExists);
            }

            // Hash the password
            var hashedPassword = passwordHasherProvider.HashPassword(request.UserPassword, out var salt);

            var newUser = User.Create(
                new CreateUserInput
                {
                    Name = request.UserName,
                    LastName = request.UserLastName,
                    Email = request.UserEmail,
                    PasswordHash = hashedPassword,
                    PasswordSalt = salt,
                    DateOfBirth = request.UserBirthDate
                }
            );

            var userCreateEvent = UserEvent.CreateUserCreatedEvent(newUser.Id!);

            var resultCreate = await mediator.Send(new CreateUserCommand(newUser, [userCreateEvent]), cancellationToken);

            if (!resultCreate.IsSuccess)
            {
                return Result.Failure<RegisterUserUseCaseOutput>(resultCreate.Error);
            }

            await eventsPublisherProvider.PublishEventsAsync(newUser, [userCreateEvent], cancellationToken);

            return new RegisterUserUseCaseOutput(newUser.Id!.Value);
        }
    }
}