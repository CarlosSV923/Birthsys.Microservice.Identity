using Birthsys.Identity.Application.Abstractions;

namespace Birthsys.Identity.Application.UseCases.GetUserById
{
    public record GetUserByIdUseCaseInput(string Id) : IUseCase<GetUserByIdUseCaseOutput>;
    public record GetUserByIdUseCaseOutput(string Id, string Email, string Name, string LastName, DateTime DateOfBirth);
}