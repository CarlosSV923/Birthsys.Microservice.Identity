using Birthsys.Identity.Application.UseCases.UpdateUser;

namespace Birthsys.Identity.Api.Controllers.UpdateUser.V1
{
    public static class UpdateUserMapper
    {
        public static UpdateUserUseCaseInput ToUseCaseInput(this UpdateUserRequest request)
        {
            return new UpdateUserUseCaseInput(
                Id: request.UserId,
                Name: request.UserName,
                LastName: request.UserLastName,
                Email: request.UserEmail,
                DateOfBirth: request.UserBirthDate
            );
        }

        public static UpdateUserResponse ToResponse(this UpdateUserUseCaseOutput output)
        {
            return new UpdateUserResponse(
                UserId: output.UserId
            );
        }
    }
}