using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;

namespace Birthsys.Identity.Application.Events.LoginUser
{
    public record LoginUserEvent(User User, UserEvent UserEvent) : INotification;
}