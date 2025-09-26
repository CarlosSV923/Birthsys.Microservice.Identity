namespace Birthsys.Identity.Application.UseCases.LoginUser
{
    using Birthsys.Identity.Application.Options;
    using FluentValidation;
    using Microsoft.Extensions.Options;
    using System.Text.RegularExpressions;

    public class LoginUserUseCaseValidator : AbstractValidator<LoginUserUseCaseInput>
    {
        public LoginUserUseCaseValidator(IOptions<PasswordOptions> passwordOptions)
        {
            var options = passwordOptions.Value;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(options.MinLength).WithMessage($"Password must be at least {options.MinLength} characters long.")
                .Matches(new Regex(options.Regex)).WithMessage("Password does not meet complexity requirements.");
        }
    }
}