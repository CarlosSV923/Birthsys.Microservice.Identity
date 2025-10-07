namespace Birthsys.Identity.Application.Consumers.UpdateUserEvent
{
    public sealed record UpdateUserEventMessage(string EventId, string EventStatus, string EventDetails);
}