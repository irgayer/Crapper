using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Crapper.DTOs.User;
using Crapper.Interfaces;
using Crapper.Models;
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
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;

        public UserController(IRepository<User> userRepository, IMapper mapper, IOptions<JwtOptions> jwtOptions)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Register(UserRegistrationDto req)
        {
            var user = _mapper.Map<User>(req);

            _userRepository.Add(user);
            _userRepository.Save();

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login(UserLoginDto req)
        {
            var user = _userRepository.Find(user => user.Email == req.Email && user.Password == req.Password).SingleOrDefault();

            if (user == null)
                return BadRequest();

            var now = DateTime.Now;
            var identity = GetIdentity(user);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                notBefore:DateTime.Now,
                claims: identity.Claims,
                expires: now.AddMinutes(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(encodedJwt);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GigaSecretRoute()
        {
            return Ok(User.Identity.Name);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserById(int id)
        {
            //todo: fix bullshit
            var user = _userRepository.Find(user => user.Id == id)
                .Include(user => user.Subscribers)
                .Include(user => user.Subscriptions)
                .Include(user => user.Posts)
                .SingleOrDefault();
            if (user == null)
                return NotFound();

            var res = _mapper.Map<UserDto>(user);

            return Ok(res);
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token");

            return claimsIdentity;
        }
    }
}
