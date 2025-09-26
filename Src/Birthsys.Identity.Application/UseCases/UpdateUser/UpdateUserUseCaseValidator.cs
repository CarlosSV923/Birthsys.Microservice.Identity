using System.Globalization;
using FluentValidation;

namespace Birthsys.Identity.Application.UseCases.UpdateUser
{
    public class UpdateUserUseCaseValidator : AbstractValidator<UpdateUserUseCaseInput>
    {
        public UpdateUserUseCaseValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User ID is required.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Invalid User ID format.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(50).WithMessage("User name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("User last name is required.")
                .MaximumLength(50).WithMessage("User last name must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.DateOfBirth)
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