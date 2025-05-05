using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Return.Create.CreateReturn;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store1CreateSale;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store1CreateStock;
using MultiStoreIntegration.Application.Features.Commands.Stock.Update.Store1UpdateStock;

namespace Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Store1Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public Store1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("StockCreate")]
        public async Task<IActionResult> CreateStock(Store1CreateStockCommandRequest createStockCommandRequest)
        {
            Store1CreateStockCommandResponse response = await _mediator.Send(createStockCommandRequest);

            return Ok(response);

        }

        [HttpPut("StockUpdate")]
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


        [HttpPost("SaleCreate")]
        public async Task<IActionResult> CreateSaleForStore1([FromBody] Store1CreateSaleCommandRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _mediator.Send(request);
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }



        [HttpPost("ReturnCreate")]

        public async Task<IActionResult> CreateReturnStore1([FromBody] Store1CreateReturnCommandRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _mediator.Send(request);
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }




    }
}
