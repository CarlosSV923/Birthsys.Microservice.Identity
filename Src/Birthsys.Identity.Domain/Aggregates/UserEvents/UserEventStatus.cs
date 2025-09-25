namespace Birthsys.Identity.Domain.Aggregates.UserEvents
{
    public class UserEventStatus
    {
        public string Code { get; init; }
        private UserEventStatus(string code)
        {
            Code = code;
        }

        public static readonly UserEventStatus Created = new("CREATED");
        public static readonly UserEventStatus Pending = new("PENDING");
        public static readonly UserEventStatus Processed = new("PROCESSED");
        public static readonly UserEventStatus Failed = new("FAILED");
        public static readonly UserEventStatus Unknown = new("UNKNOWN");

        public static readonly IReadOnlyCollection<UserEventStatus> AllStatuses =
        [
            Created,
            Pending,
            Processed,
            Failed
        ];

        public static UserEventStatus FromCode(string code)
        {
            var status = AllStatuses.FirstOrDefault(s => s.Code == code);
            return status ?? Unknown;
        }

    }
}