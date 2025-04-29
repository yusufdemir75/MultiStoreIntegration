using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Stock.Store3CreateStock;

namespace Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Store3StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Constructor injection for IMediator
        public Store3StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // API endpoint to create stock in Store3 MongoDB
        [HttpPost("create")]
        public async Task<IActionResult> CreateStock([FromBody] Store3CreateStockCommandRequest request)
        {
            // Send the request to MediatR to handle it
            var response = await _mediator.Send(request);

            // Check if the stock creation was successful and return the appropriate response
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
