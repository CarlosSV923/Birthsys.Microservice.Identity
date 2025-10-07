using Birthsys.Identity.Application.Commands.UserEvents.UpdateRange;
using Birthsys.Identity.Application.Commands.Users.Update;
using Birthsys.Identity.Application.Queries.UserEvents.FindById;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using MediatR;
using SlimMessageBus;

namespace Birthsys.Identity.Application.Consumers.UpdateUserEvent
{
    public sealed class UpdateUserEventConsumer (
        IMediator mediator
    ): IConsumer<UpdateUserEventMessage>
    {
        public async Task OnHandle(UpdateUserEventMessage message, CancellationToken cancellationToken)
        {
            var userEventFindResult = await mediator.Send(new UserEventFindByIdQuery(UserEventId.FromString(message.EventId)), cancellationToken);
            if (!userEventFindResult.IsSuccess)
            {
                Console.WriteLine(userEventFindResult.Error);
                return;
            }

            var userEvent = userEventFindResult.Value!;
            userEvent.UpdateStatus(UserEventStatus.FromCode(message.EventStatus), message.EventDetails);

            var userEventUpdateResult = await mediator.Send(new UpdateUserEventsCommand([userEvent]), cancellationToken);
            if (!userEventUpdateResult.IsSuccess)
            {
                Console.WriteLine(userEventUpdateResult.Error);
                return;
            }

            Console.WriteLine("UserEvent actualizado correctamente.");
        }
    }
}