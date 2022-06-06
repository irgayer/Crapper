using MediatR;

namespace Crapper.Features.SubscriptionsFeatures.Commands.AddSubscription
{
    public class AddSubscriptionCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int ToId { get; set; }

        public AddSubscriptionCommand(int userId, int toId)
        {
            UserId = userId;
            ToId = toId;
        }
    }
}
