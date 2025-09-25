namespace Birthsys.Identity.Application.Options
{
    public class PasswordOptions
    {
        public const string SectionName = "Password";
        public int MinLength { get; set; } = 8;
        public string Regex { get; set; }  = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"; // At least one lowercase letter, one uppercase letter, one digit, and one special character
    }
}
