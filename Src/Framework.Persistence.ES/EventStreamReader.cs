using EventStore.ClientAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Persistence.ES
{
    internal static class EventStreamReader
    {
        public static async Task<List<ResolvedEvent>> Read(IEventStoreConnection connection, string streamId, int start, int end)
        {
            var streamEvents = new List<ResolvedEvent>();

            StreamEventsSlice currentSlice;
            long nextSliceStart = start;
            do
            {
                currentSlice = await connection.ReadStreamEventsForwardAsync(streamId, nextSliceStart, end, false);

                nextSliceStart = currentSlice.NextEventNumber;

                streamEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);

            return streamEvents;
        }
    }
}
