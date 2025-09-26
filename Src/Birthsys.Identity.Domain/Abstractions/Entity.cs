namespace Birthsys.Identity.Domain.Abstractions
{
    public abstract class Entity<TEntityId>
    {
        private protected Entity() { }
        public TEntityId? Id { get; protected set; }
    }
}