using Crapper.Features.SubscriptionsFeatures.Commands.AddSubscription;
using Crapper.Features.SubscriptionsFeatures.Commands.DeleteSubscription;
using Crapper.Filters;
using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace Crapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubscriptionsController : ControllerBase
    {      
        private readonly ISender _mediator;

        public SubscriptionsController(ISender mediator)
        {
            _mediator = mediator;
        }

        //todo: delete boilerplate
        [HttpPost("user/{id}")]
        [ServiceFilter(typeof(ValidateEntityExists<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Subscribe(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var success = await _mediator.Send(new AddSubscriptionCommand(userId, id));
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("user/{id}")]
        [ServiceFilter(typeof(ValidateEntityExists<User>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Unsubscribe(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var success = await _mediator.Send(new DeleteSubscriptionCommand(userId, id));
            if (!success)
                return BadRequest();

            return Ok();
        }

    }
}
