using AuctionManagement.Domain.Contracts.Auctions;
using AuctionManagement.Domain.Model.Auctions.Events;
using AuctionManagement.Projections.TestProjection.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionManagement.Projections.TestProjection.Handlers
{
    public class AuctionOpenedHandler : IEventHandler<AuctionOpened>
    {
        public void Handle(AuctionOpened domainEvent)
        {
            //Update auction table
        }
    }

    public class BidPlacedHandler : IEventHandler<BidPlaced>
    {
        public void Handle(BidPlaced domainEvent)
        {
            //update bid table
        }
    }
}
