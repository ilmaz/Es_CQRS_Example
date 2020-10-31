using System;
using System.Collections.Generic;

namespace Framework.Domain
{
    public class AggregateFactory : IAggregateFactory
    {
        public T Create<T>(List<DomainEvent> events) where T : IAggreteRoot
        {
            var aggregate = (T)Activator.CreateInstance(typeof(T), true);

            foreach (var domainEvent in events)
            {
                aggregate.Apply(domainEvent);
            }

            return aggregate;
        }
    }
}
