using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Domain
{
    public class EventSourceRepository<T, TKey> : IEventSourceRepository<T, TKey> where T : AggregateRoot<TKey>
    {
        private readonly IEventStore _eventStore;
        private readonly IAggregateFactory _aggregateFactory;
        public EventSourceRepository(IEventStore eventStore, IAggregateFactory aggregateFactory)
        {
            _eventStore = eventStore;
            _aggregateFactory = aggregateFactory;
        }
        public async Task<T> GetById(TKey id)
        {
            var listOfEvents = await _eventStore.GetEventsOfStream(GetStreamName(id));
            return _aggregateFactory.Create<T>(listOfEvents);
        }

        public async Task AppendEvents(T aggregate)
        {
            var events = aggregate.GetUncommittedEvents();
            await _eventStore.AppendEvents(GetStreamName(aggregate.Id), events);
        }

        private string GetStreamName(TKey id)
        {
            var type = typeof(T).Name;
            return $"{type}-{id}";
        }
    }
}