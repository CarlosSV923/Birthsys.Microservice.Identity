using Birthsys.Identity.Application.Abstractions;

namespace Birthsys.Identity.Application.UseCases.LoginUser
{
    public record LoginUserUseCaseInput(string Email, string Password) : IUseCase<LoginUserUseCaseOutput>;
    public record LoginUserUseCaseOutput(string Token, DateTime Expiration);
}