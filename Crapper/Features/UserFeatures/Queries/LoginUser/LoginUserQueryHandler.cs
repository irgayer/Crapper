using AutoMapper;

using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Crapper.Features.UserFeatures.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserQueryResult>
    {
        private readonly IRepository<User> _userRepository;
        private readonly JwtOptions _jwtOptions;

        public LoginUserQueryHandler(IRepository<User> userRepository, IOptions<JwtOptions> jwtOptions)
        {
            _userRepository = userRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public Task<LoginUserQueryResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var user = _userRepository.Find(user => user.Email == dto.Email && user.Password == dto.Password).SingleOrDefault();

            if (user == null)
                return Task.FromResult(new LoginUserQueryResult() { Success = false });

            var now = DateTime.Now;
            var identity = GetIdentity(user);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                notBefore: DateTime.Now,
                claims: identity.Claims,
                expires: now.AddMinutes(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Task.FromResult(new LoginUserQueryResult() { Jwt = encodedJwt, Success = true });
        }
        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token");

            return claimsIdentity;
        }
    }
}
