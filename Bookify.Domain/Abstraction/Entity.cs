namespace Bookify.Domain.Abstraction
{
    public abstract class Entity
    {
        public Guid Id { get; init; }

        protected Entity(Guid id) => Id = id;

        private readonly List<IDomainEvent> _events;

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _events;
        }

        public void ClearDomainEvents() => _events.Clear();

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }
    }
}
