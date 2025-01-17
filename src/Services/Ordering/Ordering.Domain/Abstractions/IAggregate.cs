namespace Ordering.Domain.Abstractions
{
    public interface IAggregate<T> : IEntiy<T>, IAggregate
    {

    }
    public interface IAggregate : IEntity
    {
        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
        public IDomainEvent[] ClearDomainEvents();
    }
}
