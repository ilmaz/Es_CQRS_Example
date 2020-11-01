using System;
using System.Threading.Tasks;
using AuctionManagement.Application.Contracts;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagement.Gateways.RestApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionsController : Controller
    {
        private readonly ICommandBus _bus;
        public AuctionsController(ICommandBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OpenAuction command)
        {
            await _bus.Dispatch(command);
            return Ok();
        }

        [HttpPost("{id}/Bids")]
        public async Task<IActionResult> Post(Guid id, PlaceBid command)
        {
            command.AuctionId = id;
            await _bus.Dispatch(command);
            return Ok();
        }
    }
}