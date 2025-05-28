using Microsoft.AspNetCore.Mvc;
using MediatR;
using MultiStoreIntegration.Application.Features.Commands.User.Login.AuthLogin;

namespace MultiStoreIntegration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (string.IsNullOrEmpty(response.Token))
                return Unauthorized(response.Message);

            return Ok(response);
        }
    }
}
