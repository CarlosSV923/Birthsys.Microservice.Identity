namespace Birthsys.Identity.Api.Controllers.ChangeUserPassword.V1
{
    public record ChangeUserPasswordRequest(
        string UserId,
        string OldPassword,
        string NewPassword
    );
}