using System;
using AuctionManagement.Application.Contracts;
using AuctionManagement.Domain.Model.Auctions;
using Framework.Application;

namespace AuctionManagement.Application
{
    public class AuctionCommandHandlers : ICommandHandler<OpenAuction>,
                                          ICommandHandler<PlaceBid>
    {
        private readonly IAuctionRepository _auctionRepository;
        public AuctionCommandHandlers(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }
        public void Handle(OpenAuction cmd)
        {
            var id = Guid.NewGuid();
            var auction = new Auction(id, cmd.SellerId, cmd.StartingPrice, cmd.Product, cmd.EndDate);
            _auctionRepository.Add(auction);
        }

        public void Handle(PlaceBid command)
        {
            var auction = _auctionRepository.Get(command.AuctionId);
            auction.PlaceBid(command.BidderId, command.Amount);
            _auctionRepository.Update(auction);
        }
    }
}
