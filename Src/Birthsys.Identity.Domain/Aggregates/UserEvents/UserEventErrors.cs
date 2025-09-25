using Birthsys.Identity.Domain.Abstractions;

namespace Birthsys.Identity.Domain.Aggregates.UserEvents
{
    public static class UserEventErrors
    {
        private static Error Build(int statusCode, string code, string message)
        {
            return Error.Build(statusCode, $"UserEvent.{code}", message);
        }
        public static Error UserEventNotFound => Build(404, "NotFound", "The specified user event was not found.");
        public static Error ErrorCreatingUserEvent => Build(500, "Create", "An error occurred while creating the user event.");
        public static Error ErrorUpdatingUserEvent => Build(500, "Update", "An error occurred while updating the user event.");
    }
}