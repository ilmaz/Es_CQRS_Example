using System;
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
        public Auction Get(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Add(Auction auction)
        {
            _repository.AppendEvents(auction);
        }

        public void Update(Auction auction)
        {
            _repository.AppendEvents(auction);
        }
    }
}
