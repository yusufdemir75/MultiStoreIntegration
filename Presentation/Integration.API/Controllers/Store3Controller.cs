using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Return.Create.Store3CreateReturn;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store3CreateSale;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store3CreateStock;
using MultiStoreIntegration.Application.Features.Commands.Stock.Update.Store3UpdateStock;
using MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store3GetAllStock;
using MultiStoreIntegration.Application.Features.Queries.Stock.GetCategoryStock.Store3GetCategoryStock;

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

        [HttpPut("StockUpdate")]
        public async Task<IActionResult> UpdateStore3Stock([FromBody] Store3UpdateStockCommandRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.ProductCode))
            {
                return BadRequest("Geçerli bir güncelleme isteği gönderilmelidir.");
            }

            var result = await _mediator.Send(request);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result);
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

        [HttpGet("StockGetAll")]
        public async Task<IActionResult> GetAllStore3Stocks()
        {
            var response = await _mediator.Send(new Store3GetAllStockQueryRequest());
            return Ok(response);
        }



        [HttpGet("GetCategoryStocks")]
        public async Task<IActionResult> GetCategoryStocks()
        {
            var response = await _mediator.Send(new Store3GetCategoryStockQueryRequest());
            return response.Success ? Ok(response) : BadRequest(response);
        }

    }
}
