using Birthsys.Identity.Domain.Abstractions;

namespace Birthsys.Identity.Domain.Aggregates.UserEvents
{
    public sealed record UserEventId
    {
        public Guid Value { get; init; }

        private UserEventId(Guid value)
        {
            Value = value;
        }

        public static UserEventId New() => new(Guid.NewGuid());
        public static UserEventId FromGuid(Guid value) => new(value);
        public static UserEventId FromString(string value) => new(Guid.Parse(value));
    }
}