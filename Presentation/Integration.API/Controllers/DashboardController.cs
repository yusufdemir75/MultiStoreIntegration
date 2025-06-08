using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Infrastructure.Features.Queries.GetCategoryStockRedis;
using MultiStoreIntegration.Infrastructure.Features.Queries.GetTotalPriceSaleRedis;

namespace MultiStoreIntegration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("categoryStockTotals")]
        public async Task<IActionResult> GetCategoryStockTotals()
        {
            var result = await _mediator.Send(new GetCategoryStockRedisQueryRequest());
            return Ok(result);
        }

        [HttpGet("weekly-total-sales")]
        public async Task<IActionResult> GetWeeklyTotalSales()
        {
            var result = await _mediator.Send(new GetTotalPriceSaleRedisQueryRequest());
            return Ok(result);
        }
    }
}
