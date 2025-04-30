using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store1CreateStock;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store2CreateStock;

namespace Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Store2StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public Store2StockController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateStock(Store2CreateStockCommandRequest createStockCommandRequest)
        {
            Store2CreateStockCommandResponse response = await _mediator.Send(createStockCommandRequest);

            return Ok(response);

        }

    }
}
