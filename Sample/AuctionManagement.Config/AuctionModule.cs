using System;
using System.Linq;
using AuctionManagement.Application;
using AuctionManagement.Domain.Contracts.Auctions;
using AuctionManagement.Domain.Model.Auctions;
using AuctionManagement.Persistence.ES;
using Autofac;
using EventStore.ClientAPI;
using Framework.Application;
using Framework.Domain;
using Framework.Persistence.ES;

namespace AuctionManagement.Config
{
    public class AuctionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //TODO: --------------------------MOVE TO FRAMEWORK-------------
            builder.Register(CreateEventStoreConnection).SingleInstance();
            builder.RegisterType<EventStoreDb>().As<IEventStore>().SingleInstance();
            builder.RegisterType<AutofacCommandBus>().As<ICommandBus>().SingleInstance();
            builder.RegisterType<AggregateFactory>().As<IAggregateFactory>().SingleInstance();
            builder.RegisterType<EventTypeResolver>().As<IEventTypeResolver>()
                .SingleInstance().OnActivated(a =>
                {
                    a.Instance.AddTypesFromAssembly(typeof(BidPlaced).Assembly);
                });
            //TODO: --------------------------MOVE TO FRAMEWORK-------------

            builder.RegisterGeneric(typeof(EventSourceRepository<,>))
                .As(typeof(IEventSourceRepository<,>)).SingleInstance();

            builder.RegisterType<AuctionRepository>().As<IAuctionRepository>().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(AuctionCommandHandlers).Assembly)
                .As(type => type.GetInterfaces()
                                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))))
                .InstancePerLifetimeScope();
        }

        public static IEventStoreConnection CreateEventStoreConnection(IComponentContext context)
        {
            var connection = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
            connection.ConnectAsync().Wait();

            return connection;
        }
    }
}
