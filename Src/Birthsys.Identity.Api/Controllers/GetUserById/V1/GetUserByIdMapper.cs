using Birthsys.Identity.Application.UseCases.GetUserById;

namespace Birthsys.Identity.Api.Controllers.GetUserById.V1
{
    public static class GetUserByIdMapper
    {
        public static GetUserByIdResponse ToResponse(this GetUserByIdUseCaseOutput output)
        {
            return new GetUserByIdResponse(
                output.Id,
                output.Email,
                output.Name,
                output.LastName,
                output.DateOfBirth
            );
        }

        public static GetUserByIdUseCaseInput ToUseCaseInput(this GetUserByIdRequest request)
        {
            return new GetUserByIdUseCaseInput(request.Id);
        }

    }
}