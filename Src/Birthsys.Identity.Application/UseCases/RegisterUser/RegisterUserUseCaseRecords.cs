using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Application.UseCases.RegisterUser
{
    public record RegisterUserUseCaseInput(
        string UserName,
        string UserLastName,
        string UserEmail,
        string UserPassword,
        string UserBirthDate
    ) : IUseCase<RegisterUserUseCaseOutput>;

    public record RegisterUserUseCaseOutput(string UserId);
}