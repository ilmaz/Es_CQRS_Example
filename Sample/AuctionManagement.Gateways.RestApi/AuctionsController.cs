using System;
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
        public IActionResult Post([FromBody] OpenAuction command)
        {
            _bus.Dispatch(command);
            return Ok();
        }

        [HttpPost("{id}/Bids")]
        public IActionResult Post(Guid id, PlaceBid command)
        {
            command.AuctionId = id;
            _bus.Dispatch(command);
            return Ok();
        }
    }
}
