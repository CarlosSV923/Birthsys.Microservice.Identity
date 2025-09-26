namespace Birthsys.Identity.Api.Controllers.RegisterUser.V1
{
    public record RegisterUserRequest(
        string UserName,
        string UserLastName,
        string UserEmail,
        string UserPassword,
        string UserBirthDate
    );
}