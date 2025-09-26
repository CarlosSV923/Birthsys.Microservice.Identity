using Birthsys.Identity.Application.UseCases.GetUserByEmail;

namespace Birthsys.Identity.Api.Controllers.GetUserByEmail.V1
{
    public static class GetUserByEmailMapper
    {
        public static GetUserByEmailResponse ToResponse(this GetUserByEmailUseCaseOutput output)
        {
            return new GetUserByEmailResponse(
                UserId: output.Id,
                Email: output.Email,
                Name: output.Name,
                LastName: output.LastName,
                DateOfBirth: output.DateOfBirth
            );
        }

        public static GetUserByEmailUseCaseInput ToUseCaseInput(this GetUserByEmailRequest request)
        {
            return new GetUserByEmailUseCaseInput(request.Email);
        }
    }
}