using Crapper.DTOs.User;

using MediatR;

namespace Crapper.Features.UserFeatures.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
