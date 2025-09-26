using Birthsys.Identity.Application.UseCases.RegisterUser;

namespace Birthsys.Identity.Api.Controllers.RegisterUser.V1
{
    public static class RegisterUserMapper
    {
        public static RegisterUserUseCaseInput ToUseCaseInput(this RegisterUserRequest request)
        {
            return new RegisterUserUseCaseInput(
                request.UserName,
                request.UserLastName,
                request.UserEmail,
                request.UserPassword,
                request.UserBirthDate
            );
        }

        public static RegisterUserResponse ToResponse(this RegisterUserUseCaseOutput output)
        {
            return new RegisterUserResponse(output.UserId);
        }
    }
}