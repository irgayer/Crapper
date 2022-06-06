using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.SubscriptionsFeatures.Commands.AddSubscription
{
    public class AddSubscriptionCommandHandler : IRequestHandler<AddSubscriptionCommand, bool>
    {
        private readonly IRepository<Subscription> _subscriptionRepository;
      
        public AddSubscriptionCommandHandler(IRepository<Subscription> subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<bool> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var exists = _subscriptionRepository
                .Find(s => s.FromId == request.UserId && s.ToId == request.ToId)
                .SingleOrDefault();

            if (exists != null)
                return false;

            var subscription = new Subscription() { FromId = request.UserId, ToId = request.ToId };
            await _subscriptionRepository.Add(subscription);
            await _subscriptionRepository.Save();

            return true;
        }
    }
}
