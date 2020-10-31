using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionManagement.Domain.Model.Auctions.Events
{
    public class AuctionOpened : DomainEvent
    {
        public Guid Id { get; private set; }
        public long SellerId { get; private set; }
        public long StartingPrice { get; private set; }
        public string Product { get; private set; }
        public DateTime EndDate { get; private set; }
        public AuctionOpened(Guid id, long sellerId, long startingPrice, DateTime endDate, string product)
        {
            Id = id;
            SellerId = sellerId;
            StartingPrice = startingPrice;
            EndDate = endDate;
            Product = product;
        }
    }
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
    public class AuctionClosed
    {
    }
    public class WinnerIsChosen
    {

    }
}
