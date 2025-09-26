namespace Birthsys.Identity.Api.Controllers.GetUserByEmail.V1
{
    public record GetUserByEmailResponse(string UserId, string Email, string Name, string LastName, DateTime DateOfBirth);
}