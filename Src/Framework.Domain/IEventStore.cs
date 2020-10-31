using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain
{
    public interface IEventStore
    {
        Task<List<DomainEvent>> GetEventsOfStream(string streamId);
        Task AppendEvents(string streamId, IEnumerable<DomainEvent> events);
    }
}
