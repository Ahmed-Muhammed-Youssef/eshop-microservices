
namespace Ordering.Domain.Abstractions
{
    public abstract class Aggregate<TID> : Entity<TID>, IAggregate<TID>
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public IDomainEvent[] ClearDomainEvents()
        {
            IDomainEvent[] resultDomainEvents = [.. _domainEvents];

            _domainEvents.Clear();

            return resultDomainEvents;
        }

        public void AddDominEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
