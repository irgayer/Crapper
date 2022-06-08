using Crapper.DTOs.User;
using Crapper.Features.UserFeatures.Commands.AddUser;
using Crapper.Features.UserFeatures.Queries.LoginUser;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Crapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _mediator;

        public AuthController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(UserRegistrationDto req)
        {
            var success = await _mediator.Send(new AddUserCommand(req));
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(UserLoginDto req)
        {
            var result = await _mediator.Send(new LoginUserQuery(req));
            if (!result.Success)
                return BadRequest();

            return Ok(result.Jwt);
        }
    }
}
