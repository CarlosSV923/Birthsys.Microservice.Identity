namespace Birthsys.Identity.Domain.Aggregates.UserEvents
{
    public class UserEventProcess
    {
        public string Code { get; init; }
        private UserEventProcess(string code)
        {
            Code = code;
        }

        public static readonly UserEventProcess CreateUser = new("CreateUser");
        public static readonly UserEventProcess UpdateUser = new("UpdateUser");
        public static readonly UserEventProcess DeleteUser = new("DeleteUser");
        public static readonly UserEventProcess ChangeUserPassword = new("ChangeUserPassword");
        public static readonly UserEventProcess LoginUser = new("LoginUser");
        public static readonly UserEventProcess LogoutUser = new("LogoutUser");
        public static readonly UserEventProcess AuthenticateUser = new("AuthenticateUser");
        public static readonly UserEventProcess Unknown = new("Unknown");

        public static readonly IReadOnlyCollection<UserEventProcess> AllEventProcesss =
        [
            CreateUser,
            UpdateUser,
            DeleteUser,
            ChangeUserPassword,
            LoginUser,
            AuthenticateUser,
            Unknown
        ];

        public static UserEventProcess FromCode(string code)
        {
            var eventProcess = AllEventProcesss.FirstOrDefault(et => et.Code == code);
            return eventProcess ?? Unknown;
        }
        
    }
}