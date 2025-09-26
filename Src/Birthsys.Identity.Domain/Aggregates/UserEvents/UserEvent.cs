using System.Data;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Domain.Aggregates.UserEvents
{
    public sealed class UserEvent : Entity<UserEventId>
    {
        private UserEvent() { }
        public UserId? UserId { get; private set; }
        public string? EventDetails { get; private set; }
        public DateTime? OccurredAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public UserEventProcess? Process { get; private set; }
        public UserEventProcessResult? ProcessResult { get; private set; }
        public string? ProcessDetails { get; private set; }
        public UserEventStatus? Status { get; private set; }


        public static UserEvent Create(UserId userId, UserEventProcess process, UserEventProcessResult processResult, string? processDetails = null)
        {
            return new UserEvent
            {
                Id = UserEventId.New(),
                UserId = userId,
                Process = process,
                Status = UserEventStatus.Created,
                OccurredAt = DateTime.UtcNow,
                ProcessResult = processResult,
                ProcessDetails = processDetails
            };
        }

        public static UserEvent Create(UserId userId, UserEventProcess process, UserEventProcessResult processResult, Error error)
        {
            return Create(userId, process, processResult, error.ToString());
        }

        public static UserEvent CreateLoginSuccessEvent(UserId userId)
        {
            return Create(userId, UserEventProcess.LoginUser, UserEventProcessResult.Success, "User logged in successfully.");
        }

        public static UserEvent CreateLoginFailedEvent(UserId userId, Error error)
        {
            return Create(userId, UserEventProcess.LoginUser, UserEventProcessResult.Failed, error);
        }

        public static UserEvent CreateAuthenticateSuccessEvent(UserId userId)
        {
            return Create(userId, UserEventProcess.AuthenticateUser, UserEventProcessResult.Success, "User authenticated successfully.");
        }
        public static UserEvent CreateAuthenticateFailedEvent(UserId userId, Error error)
        {
            return Create(userId, UserEventProcess.AuthenticateUser, UserEventProcessResult.Failed, error);
        }
        public static UserEvent CreateUserCreatedEvent(UserId userId)
        {
            return Create(userId, UserEventProcess.CreateUser, UserEventProcessResult.Success, "User created successfully.");
        }
        public static UserEvent CreateUpdateUserEvent(UserId userId)
        {
            return Create(userId, UserEventProcess.UpdateUser, UserEventProcessResult.Success, "User updated successfully.");
        }
        public static UserEvent CreateChangeUserPasswordEvent(UserId userId)
        {
            return Create(userId, UserEventProcess.ChangeUserPassword, UserEventProcessResult.Success, "User password changed successfully.");
        }

        public static UserEvent CreateUserDeletedEvent(UserId userId)
        {
            return Create(userId, UserEventProcess.DeleteUser, UserEventProcessResult.Success, "User deleted successfully.");
        }
  
        public UserEvent MarkAsProcessed(string? eventDetails = null)
        {
            Status = UserEventStatus.Processed;
            UpdatedAt = DateTime.UtcNow;
            EventDetails = eventDetails;
            return this;
        }

        public UserEvent MarkAsFailed(string eventDetails)
        {
            Status = UserEventStatus.Failed;
            EventDetails = eventDetails;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public UserEvent MarkAsPending(string? eventDetails = null)
        {
            Status = UserEventStatus.Pending;
            UpdatedAt = DateTime.UtcNow;
            EventDetails = eventDetails;
            return this;
        }


    }
}