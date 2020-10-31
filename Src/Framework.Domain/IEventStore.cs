using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    public interface IEventStore
    {
        List<DomainEvent> GetEventsOfStream(string streamId);
        void AppendEvents(string streamId, IEnumerable<DomainEvent> events);
    }
}
