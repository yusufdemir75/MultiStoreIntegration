using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Stock.Store1CreateStock;

namespace Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Store1StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public Store1StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock(Store1CreateStockCommandRequest createStockCommandRequest)
        {
            Store3CreateStockCommandResponse response = await _mediator.Send(createStockCommandRequest);

            return Ok(response);
        }


    }
}
