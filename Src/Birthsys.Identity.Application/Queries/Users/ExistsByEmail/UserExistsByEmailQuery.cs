using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Application.Queries.Users.ExistsByEmail
{
    public record UserExistsByEmailQuery(UserEmail Email) : IQuery<bool>;
}