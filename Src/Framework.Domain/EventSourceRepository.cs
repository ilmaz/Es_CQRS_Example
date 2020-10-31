using System.Collections.Generic;

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
        public T GetById(TKey id)
        {
            var listOfEvents = _eventStore.GetEventsOfStream(GetStreamName(id));

            return _aggregateFactory.Create<T>(listOfEvents);
        }

        public void AppendEvents(T aggregate)
        {
            var events = aggregate.GetUncommittedEvents();
            _eventStore.AppendEvents(GetStreamName(aggregate.Id), events);
        }

        private string GetStreamName(TKey id)
        {
            var type = typeof(T).Name;
            return $"{type}-{id}";
        }
    }
}
