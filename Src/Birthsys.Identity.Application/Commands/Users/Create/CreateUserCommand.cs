using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Application.Commands.Users.Create
{
    public record CreateUserCommand(User User, IReadOnlyList<UserEvent> Events) : ICommand;
}