using System;
using System.Text;
using System.Threading.Tasks;
using AuctionManagement.Domain.Contracts.Auctions;
using AuctionManagement.Projections.TestProjection.EventHandlers;
using EventStore.ClientAPI;
using Framework.Persistence.ES;
using Newtonsoft.Json;

namespace AuctionManagement.Projections.TestProjection
{
    class Program
    {
        private static IEventTypeResolver _typeResolver = new EventTypeResolver();
        static void Main(string[] args)
        {
            var startPosition = new Position(332100399, 332100399); //TODO: just for test purpose

            _typeResolver.AddTypesFromAssembly(typeof(BidPlaced).Assembly);

            var connection = EventStoreConnection.Create(new Uri("tcp://admin:changeit@192.168.39.31:1113"));
            connection.ConnectAsync().Wait();

            connection.SubscribeToAllFrom(startPosition, CatchUpSubscriptionSettings.Default,
                EventAppeared, LiveProcessingStarted, SubscriptionDropped);

            Console.WriteLine("Subscribed !");

            Console.ReadLine();
        }

        private static void SubscriptionDropped(EventStoreCatchUpSubscription arg1, SubscriptionDropReason arg2, Exception arg3)
        {
            Console.WriteLine("Subscription Dropped !");
            Console.WriteLine("--------------------------------");
        }
        private static void LiveProcessingStarted(EventStoreCatchUpSubscription obj)
        {
            Console.WriteLine("Live Processing Started !");
            Console.WriteLine("--------------------------------");
        }
        private static Task EventAppeared(EventStoreCatchUpSubscription arg1, ResolvedEvent arg2)
        {
            if (arg2.Event.EventType.StartsWith("$")) return Task.CompletedTask;

            var typeOfEvent = _typeResolver.GetType(arg2.Event.EventType);
            if (typeOfEvent != null)
            {
                var body = Encoding.UTF8.GetString(arg2.Event.Data);
                var domainEvent = JsonConvert.DeserializeObject(body, typeOfEvent);

                IEventBus bus = null;
                bus.Publish(domainEvent);
            }
            return Task.CompletedTask;
        }
    }
}
