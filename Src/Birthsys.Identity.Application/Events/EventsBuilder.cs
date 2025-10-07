using Birthsys.Identity.Application.Events.ChangeUserPassword;
using Birthsys.Identity.Application.Events.DeleteUser;
using Birthsys.Identity.Application.Events.LoginUser;
using Birthsys.Identity.Application.Events.RegisterUser;
using Birthsys.Identity.Application.Events.UpdateUser;
using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using MediatR;

namespace Birthsys.Identity.Application.Events
{
    public sealed class EventsBuilder
    {
        private User? _user;
        private IReadOnlyList<UserEvent>? _userEvents;
        private static readonly Dictionary<string, Func<User, UserEvent, INotification>> EventFactory =
            new()
            {
                { UserEventProcess.CreateUser.Code, (user, evt) => new RegisterUserEvent(user, evt) },
                { UserEventProcess.LoginUser.Code, (user, evt) => new LoginUserEvent(user, evt) },
                { UserEventProcess.UpdateUser.Code, (user, evt) => new UpdateUserEvent(user, evt) },
                { UserEventProcess.DeleteUser.Code, (user, evt) => new DeleteUserEvent(user, evt) },
                { UserEventProcess.ChangeUserPassword.Code, (user, evt) => new ChangeUserPasswordEvent(user, evt) },
            };

        public EventsBuilder WithUser(User user)
        {
            _user = user;
            return this;
        }

        public EventsBuilder WithUserEvents(IReadOnlyList<UserEvent> userEvents)
        {
            _userEvents = userEvents;
            return this;
        }

        public List<INotification> Build()
        {
            if (_user is null)
                throw new InvalidOperationException("User must be set before building events.");

            if (_userEvents is null || !_userEvents.Any())
                throw new InvalidOperationException("UserEvents must be set and contain at least one event before building events.");

            var events = new List<INotification>();

            foreach (var userEvent in _userEvents)
            {
                if (userEvent.Process?.Code is null || !EventFactory.TryGetValue(userEvent.Process.Code, out var factory))
                {
                    throw new NotImplementedException($"Event type '{userEvent.Process?.Code}' is not implemented.");
                }

                events.Add(factory(_user, userEvent));
            }

            return events;
        }
    }
}