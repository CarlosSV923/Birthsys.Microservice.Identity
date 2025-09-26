using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;

namespace Birthsys.Identity.Application.Events.RegisterUser
{
    public record RegisterUserEvent(User User, UserEvent UserEvent) : INotification;
}