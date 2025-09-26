namespace Birthsys.Identity.Api.Controllers.GetUserById.V1
{
    public record GetUserByIdResponse(string UserId, string Email, string Name, string LastName, DateTime DateOfBirth);
}