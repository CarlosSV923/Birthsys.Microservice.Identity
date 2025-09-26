namespace Birthsys.Identity.Application.Services.Jwt
{
    public record JwtGenerateTokenResult(string Token, DateTime Expiration);
}