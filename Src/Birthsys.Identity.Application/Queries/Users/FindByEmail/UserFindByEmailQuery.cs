using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Application.Queries.Users.FindByEmail
{
    public record UserFindByEmailQuery(UserEmail Email) : IQuery<User?>;
}