using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Return.Create.Store2CreateReturn;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store2CreateSale;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store2CreateStock;
using MultiStoreIntegration.Application.Features.Commands.Stock.Update.Store2UpdateStock;
using MultiStoreIntegration.Application.Features.Queries.Sale.GetAllSale.Store1GetAllSale;
using MultiStoreIntegration.Application.Features.Queries.Sale.GetAllSale.Store2GetAllSale;
using MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store2GetAllStock;
using MultiStoreIntegration.Application.Features.Queries.Stock.GetCategoryStock.Store2GetCategoryStock;

namespace Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Store2Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public Store2Controller(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("StockCreate")]
        public async Task<IActionResult> CreateStock(Store2CreateStockCommandRequest createStockCommandRequest)
        {
            Store2CreateStockCommandResponse response = await _mediator.Send(createStockCommandRequest);

            return Ok(response);



        }

        [HttpPut("StockUpdate")]
        public async Task<IActionResult> UpdateStock(Store2UpdateStockCommandRequest updateStockCommandRequest)
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

        public async Task<IActionResult> CreateSale(Store2CreateSaleCommandRequest createSaleCommandRequest)
        {

            Store2CreateSaleCommandResponse response = await _mediator.Send(createSaleCommandRequest);

            return Ok(response);
        }

        [HttpPost("ReturnCreate")]

        public async Task<IActionResult> CreateReturn(Store2CreateReturnCommandRequest createReturnCommandRequest)
        {

            Store2CreateReturnCommandResponse response = await _mediator.Send(createReturnCommandRequest);

            return Ok(response);
        }



        [HttpGet("StockGetAll")]
        public async Task<IActionResult> GetAllStock()
        {
            var query = new Store2GetAllStockQueryRequest();
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : NotFound(response);
        }


        [HttpGet("CategoryStock")]
        public async Task<IActionResult> GetStockPerCategory()
        {
            var query = new Store2GetCategoryStockQueryRequest();
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("SaleGetAll")]
        public async Task<IActionResult> GetAllSale()
        {
            var query = new Store2GetAllSaleQueryRequest();
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : NotFound(response);
        }


    }
}
