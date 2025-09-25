namespace Birthsys.Identity.Domain.Aggregates.UserEvents
{
    public class UserEventProcessResult
    {
        public string Code { get; init; }
        private UserEventProcessResult(string code)
        {
            Code = code;
        }

        public static readonly UserEventProcessResult Success = new("Success");
        public static readonly UserEventProcessResult Failed = new("Failed");
        public static readonly UserEventProcessResult Unknown = new("Unknown");

        public static readonly IReadOnlyCollection<UserEventProcessResult> AllEventProcessResults =
        [
            Success,
            Failed
        ];

        public static UserEventProcessResult FromCode(string code)
        {
            var eventProcessResult = AllEventProcessResults.FirstOrDefault(et => et.Code == code);
            return eventProcessResult ?? Unknown;
        }

    }
}