using Asp.Versioning;
using Birthsys.Identity.Api.Utils;
using Birthsys.Identity.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Birthsys.Identity.Api.Controllers.UpdateUser.V1
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/users/update")]
    public class UpdateUserController(IMediator mediator) : ControllerBase
    {

        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(UpdateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(request.ToUseCaseInput(), cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value.ToResponse())
                : StatusCode(result.Error.Code, result.Error);
        }
    }
}