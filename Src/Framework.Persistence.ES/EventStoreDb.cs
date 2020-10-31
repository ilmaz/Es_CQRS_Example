using EventStore.ClientAPI;
using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Persistence.ES
{
    public class EventStoreDb : IEventStore
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreDb(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<DomainEvent>> GetEventsOfStream(string streamId)
        {

            var streamEvents = await EventStreamReader.Read(_connection, streamId, StreamPosition.Start, 200);

            return DomainEventFactory.Create(streamEvents);
        }

        public async Task AppendEvents(string streamId, IEnumerable<DomainEvent> events)
        {
            var eventData = EventDataFactory.CreateFromDomainEvents(events);
            await _connection.AppendToStreamAsync(streamId, ExpectedVersion.Any, eventData);
        }
    }
}
