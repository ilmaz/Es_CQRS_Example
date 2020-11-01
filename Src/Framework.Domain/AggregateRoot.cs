using System.Collections.Generic;

namespace Framework.Domain
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot
    {
        private List<DomainEvent> _uncommittedEvents;
        protected AggregateRoot()
        {
            this._uncommittedEvents = new List<DomainEvent>();    
        }
        public IReadOnlyList<DomainEvent> GetUncommittedEvents() => _uncommittedEvents.AsReadOnly();

        public void Causes(DomainEvent @event)
        {
            _uncommittedEvents.Add(@event);
            Apply(@event);
        }
        public abstract void Apply(DomainEvent @event);
    }
}