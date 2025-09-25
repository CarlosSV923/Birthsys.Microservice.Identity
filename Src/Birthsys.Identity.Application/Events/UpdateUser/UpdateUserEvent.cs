using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;
namespace Birthsys.Identity.Application.Events.UpdateUser
{
    public record UpdateUserEvent(User User, UserEvent UserEvent) : INotification;
}