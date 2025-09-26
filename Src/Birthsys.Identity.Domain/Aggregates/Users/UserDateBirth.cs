using System.Globalization;

namespace Birthsys.Identity.Domain.Aggregates.Users
{
    public record UserDateBirth
    {
        public DateTime Value { get; init; }
        private UserDateBirth(DateTime value)
        {
            Value = value;
        }
        public static UserDateBirth FromDateTime(DateTime value) => new(value);
        public static UserDateBirth FromString(string value, string format = "yyyy-MM-dd") => new(DateTime.ParseExact(value, format, CultureInfo.InvariantCulture));
    }
}