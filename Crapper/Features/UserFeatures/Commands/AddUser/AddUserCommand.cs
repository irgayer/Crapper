using Crapper.DTOs.User;

using MediatR;

namespace Crapper.Features.UserFeatures.Commands.AddUser
{
    public class AddUserCommand : IRequest<bool>
    {
        public UserRegistrationDto Dto { get; set; }

        public AddUserCommand(UserRegistrationDto dto)
        {
            Dto = dto;
        }
    }
}
