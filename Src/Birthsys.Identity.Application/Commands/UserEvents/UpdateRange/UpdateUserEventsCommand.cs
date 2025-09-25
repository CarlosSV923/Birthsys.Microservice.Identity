using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;

namespace Birthsys.Identity.Application.Commands.UserEvents.UpdateRange
{
    public record UpdateUserEventsCommand(IReadOnlyList<UserEvent> UserEvents) : ICommand;
}