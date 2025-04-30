using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store1CreateSale;

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

        [HttpPost("store1")]
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


    }
}
