using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
            connection.ConnectAsync().Wait();

            Console.WriteLine("Connected !");

            Console.WriteLine("---------------------------------");

            //AppendToStream(connection);

            var streamEvents = new List<ResolvedEvent>();

            StreamEventsSlice currentSlice;
            long nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice = connection.ReadStreamEventsForwardAsync("User-1", nextSliceStart, 200, false).Result;

                nextSliceStart = currentSlice.NextEventNumber;

                streamEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);

            foreach (var @event in streamEvents)
            {
                Console.WriteLine(Encoding.UTF8.GetString(@event.Event.Data));
            }

            Console.WriteLine("Finished !");
            Console.ReadLine();
        }

        private static void AppendToStream(IEventStoreConnection connection)
        {
            var eventPayload = new EventData(
                            eventId: Guid.NewGuid(),
                            type: "UserRegistered",
                            isJson: true,
                            data: Encoding.UTF8.GetBytes("{'username':'admin','id':1}"),
                            metadata: Encoding.UTF8.GetBytes("{}")
                        );

            connection.AppendToStreamAsync("User-1", ExpectedVersion.Any, eventPayload).Wait();
        }
    }
}
