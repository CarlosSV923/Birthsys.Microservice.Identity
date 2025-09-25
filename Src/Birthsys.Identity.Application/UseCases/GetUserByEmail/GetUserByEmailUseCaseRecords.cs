using Birthsys.Identity.Application.Abstractions;

namespace Birthsys.Identity.Application.UseCases.GetUserByEmail
{
    public record GetUserByEmailUseCaseInput(string Email) : IUseCase<GetUserByEmailUseCaseOutput>;
    public record GetUserByEmailUseCaseOutput(string Id, string Email, string Name, string LastName, DateTime DateOfBirth);
}