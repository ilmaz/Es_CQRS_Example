using System;
using System.Threading.Tasks;
using AuctionManagement.Domain.Model.Auctions;
using Framework.Domain;

namespace AuctionManagement.Persistence.ES
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly IEventSourceRepository<Auction, Guid> _repository;
        public AuctionRepository(IEventSourceRepository<Auction, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<Auction> Get(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(Auction auction)
        {
            await _repository.AppendEvents(auction);
        }

        public async Task Update(Auction auction)
        {
            await _repository.AppendEvents(auction);
        }
    }
}
