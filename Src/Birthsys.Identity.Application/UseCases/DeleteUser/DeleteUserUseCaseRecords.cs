using Birthsys.Identity.Application.Abstractions;

namespace Birthsys.Identity.Application.UseCases.DeleteUser
{
    public record DeleteUserUseCaseInput(string Id) : IUseCase<DeleteUserUseCaseOutput>;
    public record DeleteUserUseCaseOutput(string Id);
}