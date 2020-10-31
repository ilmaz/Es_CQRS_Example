using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagement.Domain.Model.Auctions
{
    public interface IAuctionRepository
    {
        Task<Auction> Get(Guid id);
        Task Add(Auction auction);
        Task Update(Auction auction);   //TODO: discuss update method
    }
}
