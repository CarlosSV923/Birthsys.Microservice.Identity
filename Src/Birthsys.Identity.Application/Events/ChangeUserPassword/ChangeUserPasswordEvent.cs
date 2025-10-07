using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;

namespace Birthsys.Identity.Application.Events.ChangeUserPassword
{
    public record ChangeUserPasswordEvent(User User, UserEvent UserEvent) : INotification;
}