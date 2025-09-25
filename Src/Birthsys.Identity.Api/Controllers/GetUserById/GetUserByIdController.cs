using Birthsys.Identity.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Birthsys.Identity.Api.Controllers.GetUserById
{
    [ApiController]
    [Route("api/users/{id}")]
    public class GetUserByIdController(
        IMediator mediator
    ) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetUserByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdRequest(id);
            var result = await mediator.Send(query.ToInput(), cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value.ToResponse())
                : StatusCode(result.Error.Code, result.Error);
        }
    }
}