using Asp.Versioning;
using Birthsys.Identity.Api.Utils;
using Birthsys.Identity.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Birthsys.Identity.Api.Controllers.ChangeUserPassword.V1
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/users/changePassword")]
    public class ChangeUserPasswordController(IMediator mediator) : ControllerBase
    {

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="request">Change user password request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ChangeUserPasswordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangeUserPasswordRequest request,
            CancellationToken cancellationToken)
        {

            var result = await mediator.Send(request.ToUseCaseInput(), cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value.ToResponse())
                : StatusCode(result.Error.Code, result.Error);
        }
    }
}