using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;
namespace Birthsys.Identity.Application.Events.AuthenticationUser
{
    public record AuthenticationUserEvent(User User, UserEvent UserEvent) : INotification;
}