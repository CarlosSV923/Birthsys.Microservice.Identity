namespace Birthsys.Identity.Domain.Aggregates.Users
{
    public record UserId
    {
        public Guid Value { get; init; }
        private UserId(Guid value)
        {
            Value = value;
        }
        public static UserId New() => new(Guid.NewGuid());
        public static UserId FromGuid(Guid value) => new(value);
        public static UserId FromString(string value) => new(Guid.Parse(value));
    }
}