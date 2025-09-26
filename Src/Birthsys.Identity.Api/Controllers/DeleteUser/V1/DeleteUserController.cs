using Asp.Versioning;
using Birthsys.Identity.Api.Utils;
using Birthsys.Identity.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Birthsys.Identity.Api.Controllers.DeleteUser.V1
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/users/delete")]
    public class DeleteUserController(
        IMediator mediator
    ) : ControllerBase
    {
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="request">Delete user request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpDelete]
        [ProducesResponseType(typeof(DeleteUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteUser(
            [FromBody] DeleteUserRequest request,
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