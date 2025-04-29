using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Sale.CreateSale;
using MultiStoreIntegration.Application.Features.Commands.Stock.Store1CreateStock;

namespace Integration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleCommandRequest createSaleCommandRequest)
        {
            CreateSaleCommandResponse response = await _mediator.Send(createSaleCommandRequest);

            return Ok(response);
        }


    }
}
