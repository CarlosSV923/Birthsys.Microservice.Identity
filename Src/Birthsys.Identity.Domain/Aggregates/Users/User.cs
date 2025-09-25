using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users.Inputs;

namespace Birthsys.Identity.Domain.Aggregates.Users
{
    public sealed class User : Entity<UserId>
    {
        private User() { }
        public UserName? Name { get; private set; }
        public UserLastName? LastName { get; private set; }
        public UserEmail? Email { get; private set; }
        public UserPasswordHash? PasswordHash { get; private set; }
        public UserPasswordSalt? PasswordSalt { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public UserDateBirth? DateOfBirth { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime? DeleteAt { get; private set; }

        public static User Create(CreateUserInput input)
        {
            return new User
            {
                Id = UserId.New(),
                Name = new UserName(input.Name!),
                LastName = new UserLastName(input.LastName!),
                Email = new UserEmail(input.Email!),
                PasswordHash = new UserPasswordHash(input.PasswordHash!),
                PasswordSalt = new UserPasswordSalt(input.PasswordSalt!),
                DateOfBirth = UserDateBirth.FromString(input.DateOfBirth!),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
        }

        public User UpdateName(string name)
        {
            Name = new UserName(name);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public User UpdateLastName(string lastName)
        {
            LastName = new UserLastName(lastName);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public User UpdateEmail(string email)
        {
            Email = new UserEmail(email);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public User UpdateDateOfBirth(string dateOfBirth)
        {
            DateOfBirth = UserDateBirth.FromString(dateOfBirth);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public User UpdatePassword(string passwordHash, string passwordSalt)
        {
            PasswordHash = new UserPasswordHash(passwordHash);
            PasswordSalt = new UserPasswordSalt(passwordSalt);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public User Deactivate()
        {
            IsActive = false;
            DeleteAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

    }
}
