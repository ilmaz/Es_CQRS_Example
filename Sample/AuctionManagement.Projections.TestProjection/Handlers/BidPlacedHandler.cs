using AuctionManagement.Domain.Contracts.Auctions;
using AuctionManagement.Projections.TestProjection.EventHandlers;

namespace AuctionManagement.Projections.TestProjection.Handlers
{
    public class BidPlacedHandler : IEventHandler<BidPlaced>
    {
        public void Handle(BidPlaced domainEvent)
        {
            //update bid table
        }
    }
}