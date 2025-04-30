using Amazon.Runtime.Internal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store1CreateStock;
using MultiStoreIntegration.Application.Features.Commands.Stock.Update.Store1UpdateStock;

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
            Store1CreateStockCommandResponse response = await _mediator.Send(createStockCommandRequest);

            return Ok(response);

        }

        [HttpPut("store1/update")]
        public async Task<IActionResult> UpdateStock(Store1UpdateStockCommandRequest updateStockCommandRequest)
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _mediator.Send(updateStockCommandRequest);

                if (response.Success)
                    return Ok(response);

                return BadRequest(response);
            }

        }
    }
}
