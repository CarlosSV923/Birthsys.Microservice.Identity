using Birthsys.Identity.Application.Abstractions;

namespace Birthsys.Identity.Application.UseCases.ChangeUserPassword
{
    public record ChangeUserPasswordUseCaseInput(string Id, string CurrentPassword, string NewPassword) : IUseCase<ChangeUserPasswordUseCaseOutput>;
    public record ChangeUserPasswordUseCaseOutput(Guid Id);
}