using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
namespace Birthsys.Identity.Application.Commands.UserEvents.CreateRange
{
    public record CreateUserEventsCommand(IReadOnlyList<UserEvent> UserEvents) : ICommand;
}