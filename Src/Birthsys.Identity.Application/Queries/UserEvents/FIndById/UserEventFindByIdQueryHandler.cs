using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.UserEvents.Interfaces;

namespace Birthsys.Identity.Application.Queries.UserEvents.FindById
{
    internal sealed class UserEventFindByIdQueryHandler(
        IUserEventRepository repository
    ) : IQueryHandler<UserEventFindByIdQuery, UserEvent?>
    {
        public async Task<Result<UserEvent?>> Handle(UserEventFindByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await repository.FindByIdAsync(request.Id, cancellationToken);
                if (result == null)
                {
                    return Result.Failure<UserEvent?>(UserEventErrors.UserEventNotFound);
                }

                return result;
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex);
                return Result.Failure<UserEvent?>(Error.Build(500, "InternalServerError", ex.Message)); 
            }
        }
    }
}