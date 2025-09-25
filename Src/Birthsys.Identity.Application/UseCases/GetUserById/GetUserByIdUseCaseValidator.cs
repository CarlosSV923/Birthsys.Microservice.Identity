using FluentValidation;

namespace Birthsys.Identity.Application.UseCases.GetUserById
{

    public class GetUserByIdUseCaseValidator : AbstractValidator<GetUserByIdUseCaseInput>
    {
        public GetUserByIdUseCaseValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User ID is required.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("User ID must be a valid GUID.");
        }
    }
}
