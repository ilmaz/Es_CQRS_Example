namespace AuctionManagement.Domain.Model.Auctions
{
    public class WinningBid
    {
        public long BidderId { get; private set; }
        public long Amount { get; private set; }

        public WinningBid(long bidderId, long amount)
        {
            BidderId = bidderId;
            Amount = amount;
        }
    }
}
