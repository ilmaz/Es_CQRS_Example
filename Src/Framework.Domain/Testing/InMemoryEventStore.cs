using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain.Testing
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<string, List<DomainEvent>> _events = new Dictionary<string, List<DomainEvent>>();
        public List<DomainEvent> GetEventsOfStream(string streamId)
        {
            return _events[streamId];
        }

        public void AppendEvents(string streamId, IEnumerable<DomainEvent> events)
        {
            if (_events.ContainsKey(streamId))
                _events[streamId].AddRange(events);
            else
                _events.Add(streamId, events.ToList());
        }
    }
}
