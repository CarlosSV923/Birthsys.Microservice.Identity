using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Application.Queries.Users.FindById
{
    public record UserFindByIdQuery(UserId Id) : IQuery<User?>;
}