using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggreteRoot
    {
        private List<DomainEvent> _uncommittedEvents;

        public AggregateRoot()
        {
            _uncommittedEvents = new List<DomainEvent>();
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
