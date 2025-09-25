using Birthsys.Identity.Domain.Abstractions;

namespace Birthsys.Identity.Domain.Aggregates.Users
{
    public static class UserErrors
    {
        private static Error Build(int statusCode, string code, string message)
        {
            return Error.Build(statusCode, $"User.{code}", message);
        }
        public static Error UserNotFound => Build(404, "NotFound", "The specified user was not found.");
        public static Error EmailAlreadyExists => Build(400, "EmailAlreadyExists", "The specified email is already in use.");
        public static Error InvalidCredentials => Build(401, "InvalidCredentials", "The provided credentials are invalid.");
        public static Error ErrorCreatingUser => Build(500, "Create", "An error occurred while creating the user.");
        public static Error ErrorUpdatingUser => Build(500, "Update", "An error occurred while updating the user.");
        public static Error ErrorDeletingUser => Build(500, "Delete", "An error occurred while deleting the user.");
        public static Error ErrorFindingUser => Build(500, "Find", "An error occurred while finding the user.");
        public static Error ErrorCheckingUserEmail => Build(500, "CheckEmail", "An error occurred while checking the user email.");
        public static Error InvalidUserId => Build(400, "InvalidId", "The specified user ID is invalid.");
        public static Error InvalidCurrentPassword => Build(400, "InvalidCurrentPassword", "The current password is incorrect.");
    }
}