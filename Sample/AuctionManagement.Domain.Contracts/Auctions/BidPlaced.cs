using System;
using Framework.Domain;

namespace AuctionManagement.Domain.Contracts.Auctions
{
    public class BidPlaced : DomainEvent
    {
        public Guid AuctionId { get; private set; }
        public long BidAmount { get; private set; }
        public long BidderId { get; private set; }
        public BidPlaced(Guid auctionId, long bidAmount, long bidderId)
        {
            AuctionId = auctionId;
            BidAmount = bidAmount;
            BidderId = bidderId;
        }
    }
}