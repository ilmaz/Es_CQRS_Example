using Framework.Domain;

namespace AuctionManagement.Projections.TestProjection.EventHandlers
{
    public interface IEventBus
    {
        void Publish(object @event);
    }
}