namespace Birthsys.Identity.Domain.Aggregates.Users.Inputs
{
    public sealed record CreateUserInput
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public string? DateOfBirth { get; set; }
    }
}