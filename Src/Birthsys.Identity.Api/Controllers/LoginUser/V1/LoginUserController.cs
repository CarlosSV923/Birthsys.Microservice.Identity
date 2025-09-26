using Asp.Versioning;
using Birthsys.Identity.Api.Utils;
using Birthsys.Identity.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Birthsys.Identity.Api.Controllers.LoginUser.V1
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/users/login")]
    public class LoginUserController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="request">Login user request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpPost("api/v{version:apiVersion}/users/login")]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [FromBody] LoginUserRequest request,
            CancellationToken cancellationToken)
        {

            var result = await mediator.Send(request.ToUseCaseInput(), cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value.ToResponse())
                : StatusCode(result.Error.Code, result.Error);
        }
    }
}