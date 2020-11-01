using AuctionManagement.Domain.Contracts.Auctions;
using AuctionManagement.Projections.TestProjection.EventHandlers;

namespace AuctionManagement.Projections.TestProjection.Handlers
{
    public class AuctionOpenedHandler : IEventHandler<AuctionOpened>
    {
        public void Handle(AuctionOpened domainEvent)
        {
            // Update auction table
        }
    }
}