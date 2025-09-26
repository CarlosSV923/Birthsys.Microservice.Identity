using FluentValidation;

namespace Birthsys.Identity.Application.UseCases.DeleteUser
{
    public class DeleteUserUseCaseValidator : AbstractValidator<DeleteUserUseCaseInput>
    {
        public DeleteUserUseCaseValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("User ID is required.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("User ID must be a valid GUID.");
        }
    }
}