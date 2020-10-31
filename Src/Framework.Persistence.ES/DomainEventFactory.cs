using EventStore.ClientAPI;
using Framework.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Persistence.ES
{
    internal static class DomainEventFactory
    {
        public static List<DomainEvent> Create(List<ResolvedEvent> events)
        {
            return events.Select(Create).ToList();
        }

        public static DomainEvent Create(ResolvedEvent @event)
        {
            var type = @event.Event.EventType;
            var body = Encoding.UTF8.GetString(@event.Event.Data);

            return (DomainEvent)JsonConvert.DeserializeObject(body, Type.GetType(type));
        }
    }
}
