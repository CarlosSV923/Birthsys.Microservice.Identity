namespace Birthsys.Identity.Api.Controllers.LoginUser.V1
{
    public record LoginUserResponse(string Token, DateTime Expiration);
}