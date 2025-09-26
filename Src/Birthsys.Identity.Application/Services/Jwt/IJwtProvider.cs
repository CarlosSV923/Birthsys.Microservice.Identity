using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;

namespace Birthsys.Identity.Application.Services.Jwt
{
    public interface IJwtProvider
    {
        Result<JwtGenerateTokenResult> GenerateToken(User user);
    }
}