using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Application.Commands.Users.Update
{
    public record UpdateUserCommand(User User, IReadOnlyList<UserEvent> Events) : ICommand;
}