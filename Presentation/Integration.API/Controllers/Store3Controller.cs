using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Return.Create.Store3CreateReturn;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store3CreateSale;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store3CreateStock;

namespace Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Store3Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        // Constructor injection for IMediator
        public Store3Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("StockCreate")]
        public async Task<IActionResult> CreateStock([FromBody] Store3CreateStockCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("SaleCreate")]
        public async Task<IActionResult> CreateSale([FromBody] Store3CreateSaleCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("ReturnCreate")]
        public async Task<IActionResult> CreateReturn([FromBody] Store3CreateReturnCommandRequest request)
        {
            var response =await _mediator.Send(request);

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
