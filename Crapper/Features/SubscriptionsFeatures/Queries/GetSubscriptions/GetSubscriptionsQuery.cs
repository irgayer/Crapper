using Crapper.DTOs.User;

using MediatR;

namespace Crapper.Features.SubscriptionsFeatures.Queries.GetSubscriptions
{
    public class GetSubscriptionsQuery : IRequest<ICollection<UserDto>>
    {
        public int UserId { get; set; }

        public GetSubscriptionsQuery(int userId)
        {
            UserId = userId;
        }
    }
}
