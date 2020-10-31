using System;
using System.Threading.Tasks;
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
        public async Task Handle(OpenAuction cmd)
        {
            var id = Guid.NewGuid();
            var auction = new Auction(id, cmd.SellerId, cmd.StartingPrice, cmd.Product, cmd.EndDate);
            await _auctionRepository.Add(auction);
        }

        public async Task Handle(PlaceBid command)
        {
            var auction = await _auctionRepository.Get(command.AuctionId);
            auction.PlaceBid(command.BidderId, command.Amount);
            await _auctionRepository.Update(auction);
        }
    }
}
