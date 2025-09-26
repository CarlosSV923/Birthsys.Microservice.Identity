namespace Birthsys.Identity.Application.Events.ChangeUserPassword
{
    using Birthsys.Identity.Domain.Aggregates.UserEvents;
    using Birthsys.Identity.Domain.Aggregates.Users;
    using MediatR;

    public record ChangeUserPasswordEvent(User User, UserEvent UserEvent) : INotification;
}