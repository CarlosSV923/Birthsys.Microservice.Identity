namespace Birthsys.Identity.Application.Producers
{
    public abstract class ProducerEventBase
    {
        public string UserId { get; } = string.Empty;
        public string Email { get; } = string.Empty;
        public string Name { get; } = string.Empty;
        public string LastName { get; } = string.Empty;
        public string EventId { get; } = string.Empty;
        public string ProcessResult { get; } = string.Empty;
    }
}