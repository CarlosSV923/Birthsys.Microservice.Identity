using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;

namespace Birthsys.Identity.Application.Events.DeleteUser
{
    public record DeleteUserEvent(User User, UserEvent UserEvent) : INotification;
}