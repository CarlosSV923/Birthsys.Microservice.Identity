using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;

namespace Birthsys.Identity.Application.Queries.UserEvents.FindById
{
    public record UserEventFindByIdQuery(UserEventId Id) : IQuery<UserEvent?>;
}
