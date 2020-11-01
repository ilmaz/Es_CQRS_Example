using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Framework.Domain;

namespace Framework.Persistence.ES
{
    public class EventStoreDb : IEventStore
    {
        private readonly IEventStoreConnection _connection;
        private readonly IEventTypeResolver _typeResolver;
        public EventStoreDb(IEventStoreConnection connection, IEventTypeResolver typeResolver)
        {
            _connection = connection;
            _typeResolver = typeResolver;
        }
        public async Task<List<DomainEvent>> GetEventsOfStream(string streamId)
        {
            var streamEvents = await EventStreamReader.Read(_connection, streamId, StreamPosition.Start, 200);
            return DomainEventFactory.Create(streamEvents, _typeResolver);
        }

        public async Task AppendEvents(string streamId, IEnumerable<DomainEvent> events)
        {
            var eventData = EventDataFactory.CreateFromDomainEvents(events);
            await _connection.AppendToStreamAsync(streamId, ExpectedVersion.Any, eventData);
        }
    }
}