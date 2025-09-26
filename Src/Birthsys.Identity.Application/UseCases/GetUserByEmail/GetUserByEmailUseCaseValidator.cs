using FluentValidation;

namespace Birthsys.Identity.Application.UseCases.GetUserByEmail
{

    public class GetUserByEmailUseCaseValidator : AbstractValidator<GetUserByEmailUseCaseInput>
    {
        public GetUserByEmailUseCaseValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}