using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiStoreIntegration.Infrastructure.Features.Queries.GetCategoryStockRedis;

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

        [HttpGet("category-stock-totals")]
        public async Task<IActionResult> GetCategoryStockTotals()
        {
            var result = await _mediator.Send(new GetCategoryStockRedisQueryRequest());
            return Ok(result);
        }
    }
}
