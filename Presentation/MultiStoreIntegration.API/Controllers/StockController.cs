using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Stock.CreateStock;

namespace MultiStoreIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock(CreateStockCommandRequest createStockCommandRequest)
        {
            CreateStockCommandResponse response = await _mediator.Send(createStockCommandRequest);

            return Ok(response);
        }


    }
}
