using System;
using AuctionManagement.Domain.Contracts.Auctions;
using Framework.Domain;

namespace AuctionManagement.Domain.Model.Auctions
{
    public partial class Auction : AggregateRoot<Guid>
    {
        public long SellerId { get; private set; }
        public long StartingPrice { get; private set; }
        public string Product { get; private set; }
        public DateTime EndDate { get; private set; }
        public WinningBid WinningBid { get; private set; }
        private Auction() { }
        public Auction(Guid id, long sellerId, long startingPrice, string product, DateTime endDate)
        {
            if (endDate < DateTime.Now) throw  new Exception("end date cant be past !");

            Causes(new AuctionOpened(id, sellerId, startingPrice, endDate, product));
        }
        public void PlaceBid(long bidderId, long amount)
        {
            var maxBid = this.StartingPrice;
            if (!FirstBid()) maxBid = this.WinningBid.Amount;
            
            if (maxBid >= amount) throw new Exception("Invalid amount");
            if (this.SellerId == bidderId) throw new Exception("invalid bidder !!!");
            //.......

            Causes(new BidPlaced(this.Id, amount, bidderId));
        }
        private bool FirstBid()
        {
            return this.WinningBid == null;
        }
    
    }
}