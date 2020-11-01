using System.Collections.Generic;

namespace Framework.Domain
{
    public interface IAggregateFactory
    {
        T Create<T>(List<DomainEvent> events) where T : IAggregateRoot;
    }
}