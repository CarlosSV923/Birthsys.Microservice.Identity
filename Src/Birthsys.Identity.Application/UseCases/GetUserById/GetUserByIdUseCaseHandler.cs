using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Application.Queries.Users.FindById;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;

namespace Birthsys.Identity.Application.UseCases.GetUserById
{
    internal sealed class GetUserByIdUseCaseHandler(
        IMediator mediator
    ) : IUseCaseHandler<GetUserByIdUseCaseInput, GetUserByIdUseCaseOutput>
    {
        public async Task<Result<GetUserByIdUseCaseOutput>> Handle(GetUserByIdUseCaseInput request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out var userId))
            {
                return Result.Failure<GetUserByIdUseCaseOutput>(UserErrors.InvalidUserId);
            }

            var userResult = await mediator.Send(new UserFindByIdQuery(UserId.FromGuid(userId)), cancellationToken);
            if (!userResult.IsSuccess)
            {
                return Result.Failure<GetUserByIdUseCaseOutput>(userResult.Error);
            }

            var user = userResult.Value!;
            return new GetUserByIdUseCaseOutput(
                user.Id!.Value.ToString(),
                user.Email!.Value,
                user.Name!.Value,
                user.LastName!.Value,
                user.DateOfBirth!.Value
            );
        }
    }
}