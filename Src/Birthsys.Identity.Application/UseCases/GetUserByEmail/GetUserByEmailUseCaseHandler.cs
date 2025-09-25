using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Application.Queries.Users.FindByEmail;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;

namespace Birthsys.Identity.Application.UseCases.GetUserByEmail
{
    internal sealed class GetUserByEmailUseCaseHandler(
        IMediator mediator
    ) : IUseCaseHandler<GetUserByEmailUseCaseInput, GetUserByEmailUseCaseOutput>
    {
        public async Task<Result<GetUserByEmailUseCaseOutput>> Handle(GetUserByEmailUseCaseInput request, CancellationToken cancellationToken)
        {
            var userResult = await mediator.Send(new UserFindByEmailQuery(new UserEmail(request.Email)), cancellationToken);
            if (!userResult.IsSuccess)
            {
                return Result.Failure<GetUserByEmailUseCaseOutput>(userResult.Error);
            }

            var user = userResult.Value!;
            return new GetUserByEmailUseCaseOutput(
                user.Id!.Value.ToString(),
                user.Email!.Value,
                user.Name!.Value,
                user.LastName!.Value,
                user.DateOfBirth!.Value
            );
        }
    }
}