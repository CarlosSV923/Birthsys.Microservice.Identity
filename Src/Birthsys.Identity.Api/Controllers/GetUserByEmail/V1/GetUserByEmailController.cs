using Asp.Versioning;
using Birthsys.Identity.Api.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Birthsys.Identity.Api.Controllers.GetUserByEmail.V1
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/users/getByEmail")]
    public class GetUserByEmailController(
        IMediator mediator
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetUserByEmailResponse>> GetUserByEmail([FromQuery] GetUserByEmailRequest request)
        {
            var result = await mediator.Send(request.ToUseCaseInput());

            return result.IsSuccess
                ? Ok(result.Value.ToResponse())
                : StatusCode(result.Error.Code, result.Error);
        }
    }
}