using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionManagement.Application.Contracts
{
    public class PlaceBid
    {
        public Guid AuctionId { get; set; }
        public long BidderId { get; set; }
        public long Amount { get; set; }
    }
}
