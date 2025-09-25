namespace Birthsys.Identity.Api.Controllers.GetUserById
{
    public record GetUserByIdResponse(string Id, string Email, string Name, string LastName, DateTime DateOfBirth);
}