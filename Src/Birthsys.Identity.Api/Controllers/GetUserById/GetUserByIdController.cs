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

    }
}