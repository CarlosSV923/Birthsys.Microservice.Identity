using Birthsys.Identity.Application.UseCases.LoginUser;

namespace Birthsys.Identity.Api.Controllers.LoginUser.V1
{
    public static class LoginUserMapper
    {
        public static LoginUserUseCaseInput ToUseCaseInput(this LoginUserRequest request)
        {
            return new LoginUserUseCaseInput(request.Email, request.Password);
        }

        public static LoginUserResponse ToResponse(this LoginUserUseCaseOutput response)
        {
            return new LoginUserResponse(response.Token, response.Expiration);
        }
    }
}