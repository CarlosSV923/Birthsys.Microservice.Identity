namespace Birthsys.Identity.Api.Controllers.UpdateUser.V1
{
    public record UpdateUserRequest(
        string UserId,
        string UserName,
        string UserLastName,
        string UserEmail,
        string UserBirthDate
    );
}