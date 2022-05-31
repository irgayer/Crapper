using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using AutoMapper;
using Crapper.DTOs.User;
using Crapper.Features.UserFeatures.Commands.AddUser;
using Crapper.Features.UserFeatures.Queries.GetUserById;
using Crapper.Features.UserFeatures.Queries.LoginUser;
using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Crapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediator;

        public UserController(ISender mediator = null)
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

        [HttpGet("whoami")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserIdAsync()
        {
            try
            {
                var id = int.Parse(User.FindFirstValue("id"));
                var user = await _mediator.Send(new GetUserByIdQuery(id));

                return Ok(new { id = user.Id });
            }
            catch (Exception)
            {
                return BadRequest();
            }     
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return user != null ? Ok(user) : NotFound();
        }        
    }
}
