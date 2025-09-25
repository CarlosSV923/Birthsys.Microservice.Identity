using System.Globalization;
using System.Text.RegularExpressions;
using Birthsys.Identity.Application.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Birthsys.Identity.Application.UseCases.RegisterUser
{
    public class RegisterUserUseCaseValidator : AbstractValidator<RegisterUserUseCaseInput>
    {
        public RegisterUserUseCaseValidator(
            IOptions<PasswordOptions> passwordOptions
        )
        {
            var pwdOptions = passwordOptions.Value;
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(50).WithMessage("User name must not exceed 50 characters.");

            RuleFor(x => x.UserLastName)
                .NotEmpty().WithMessage("User last name is required.")
                .MaximumLength(50).WithMessage("User last name must not exceed 50 characters.");

            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.UserPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(pwdOptions.MinLength).WithMessage($"Password must be at least {pwdOptions.MinLength} characters long.")
                .Matches (new Regex(pwdOptions.Regex))
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");

            RuleFor(x => x.UserBirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .Must(BeAValidDate).WithMessage("Invalid birth date format. Use YYYY-MM-DD.")
                .Must(BeAtLeast18YearsOld).WithMessage("User must be at least 18 years old.");
        }

        private static bool BeAValidDate(string birthDate)
        {
            return DateTime.TryParse(birthDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        private static bool BeAtLeast18YearsOld(string birthDate)
        {
            if (DateTime.TryParse(birthDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date <= DateTime.UtcNow.AddYears(-18);
            }
            return false;
        }
    }
}