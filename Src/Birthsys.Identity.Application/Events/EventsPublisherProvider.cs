using Birthsys.Identity.Application.Commands.UserEvents.CreateRange;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;

namespace Birthsys.Identity.Application.Events
{
    public sealed class EventsPublisherProvider(
        IMediator mediator
    ) : IEventsPublisherProvider
    {
        public async Task CreateAndPublishEventsAsync(User user, IReadOnlyList<UserEvent> events, CancellationToken cancellationToken = default)
        {
            var createEventsResult = await mediator.Send(new CreateUserEventsCommand(events), cancellationToken);

            if (createEventsResult.IsSuccess)
            {
                await PublishEventsAsync(user, events, cancellationToken);
            } else
            {
                // Log the error (you can use any logging framework you prefer)
                Console.WriteLine("Failed to create user events: " + createEventsResult.Error);
            }
        }

        public async Task PublishEventsAsync(User user, IReadOnlyList<UserEvent> events, CancellationToken cancellationToken = default)
        {
            try
            {
                var eventList = new EventsBuilder()
                .WithUser(user)
                .WithUserEvents(events)
                .Build();

                foreach (var @event in eventList)
                {
                    await mediator.Publish(@event, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework you prefer)
                Console.WriteLine($"An error occurred while publishing events: {ex.Message}");
            }

        }
    }
}