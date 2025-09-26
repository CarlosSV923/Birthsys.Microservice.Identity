using Asp.Versioning;
using Birthsys.Identity.Api.Utils;
using Birthsys.Identity.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Birthsys.Identity.Api.Controllers.RegisterUser.V1
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/users/register")]
    public class RegisterUserController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Register user
        /// </summary>
        /// </summary>
        /// <param name="request">Register user request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Register user response</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUserAsync(
            [FromBody] RegisterUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var result = await mediator.Send(request.ToUseCaseInput(), cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value.ToResponse())
                : StatusCode(result.Error.Code, result.Error);
        }
    }
}