using System.Text.RegularExpressions;
using Birthsys.Identity.Application.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Birthsys.Identity.Application.UseCases.ChangeUserPassword
{
    public class ChangeUserPasswordUseCaseValidator : AbstractValidator<ChangeUserPasswordUseCaseInput>
    {
        public ChangeUserPasswordUseCaseValidator(IOptions<PasswordOptions> passwordOptions)
        {
            var options = passwordOptions.Value;
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("User ID is required.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("User ID must be a valid GUID.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("New password is required.")
                .MinimumLength(options.MinLength)
                .WithMessage($"New password must be at least {options.MinLength} characters long.")
                .Matches(new Regex(options.Regex))
                .WithMessage("New password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
        }
    }
}
