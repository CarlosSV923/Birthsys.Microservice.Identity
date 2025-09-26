using Birthsys.Identity.Application.UseCases.ChangeUserPassword;

namespace Birthsys.Identity.Api.Controllers.ChangeUserPassword.V1
{
    public static class ChangeUserPasswordMapper
    {
        public static ChangeUserPasswordUseCaseInput ToUseCaseInput(this ChangeUserPasswordRequest request)
        {
            return new ChangeUserPasswordUseCaseInput(
                request.UserId,
                request.OldPassword,
                request.NewPassword
            );
        }

        public static ChangeUserPasswordResponse ToResponse(this ChangeUserPasswordUseCaseOutput output)
        {
            return new ChangeUserPasswordResponse(output.Id);
        }
    }
}