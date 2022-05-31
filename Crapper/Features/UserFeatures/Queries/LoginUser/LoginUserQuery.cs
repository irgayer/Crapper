using Crapper.DTOs.User;

using MediatR;

namespace Crapper.Features.UserFeatures.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<LoginUserQueryResult>
    {
        public UserLoginDto Dto { get; set; }

        public LoginUserQuery(UserLoginDto dto)
        {
            Dto = dto;
        }
    }
}
