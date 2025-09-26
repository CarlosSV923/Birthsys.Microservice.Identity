using Birthsys.Identity.Application.UseCases.DeleteUser;

namespace Birthsys.Identity.Api.Controllers.DeleteUser.V1
{
    public static class DeleteUserMapper
    {
        public static DeleteUserUseCaseInput ToUseCaseInput(this DeleteUserRequest input)
        {
            return new DeleteUserUseCaseInput(input.UserId);
        }

        public static DeleteUserResponse ToResponse(this DeleteUserUseCaseOutput output)
        {
            return new DeleteUserResponse(output.Id);
        }
    }
}