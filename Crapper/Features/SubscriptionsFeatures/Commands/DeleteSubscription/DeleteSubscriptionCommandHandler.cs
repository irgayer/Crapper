using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.SubscriptionsFeatures.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, bool>
    {
        private readonly IRepository<Subscription> _subscriptionRepository;

        public DeleteSubscriptionCommandHandler(IRepository<Subscription> subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<bool> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var candidate = _subscriptionRepository
                .Find(s => s.FromId == request.UserId && s.ToId == request.ToId)
                .SingleOrDefault();

            if (candidate == null)
                return false;

            _subscriptionRepository.Delete(candidate);
            await _subscriptionRepository.Save();

            return true;
        }
    }
}
