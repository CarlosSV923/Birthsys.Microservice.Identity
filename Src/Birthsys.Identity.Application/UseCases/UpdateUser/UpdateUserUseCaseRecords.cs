using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Application.UseCases.UpdateUser
{

    public record UpdateUserUseCaseInput(
        string Id,
        string Name,
        string LastName,
        string Email,
        string DateOfBirth
    )  : IUseCase<UpdateUserUseCaseOutput>;

    public record UpdateUserUseCaseOutput(string UserId);
}