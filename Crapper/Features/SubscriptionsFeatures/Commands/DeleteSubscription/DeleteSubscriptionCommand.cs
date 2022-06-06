using MediatR;

namespace Crapper.Features.SubscriptionsFeatures.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int ToId { get; set; }
        
        public DeleteSubscriptionCommand(int userId, int toId)
        {
            UserId = userId;
            ToId = toId;
        }
    }
}
