using AutoMapper;

using Crapper.DTOs.User;
using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.SubscriptionsFeatures.Queries.GetSubscriptions
{
    public class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsQuery, ICollection<UserDto>>
    {
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IMapper _mapper;

        public GetSubscriptionsQueryHandler(IRepository<User> postRepository, IRepository<Subscription> subscriptionRepository, IMapper mapper)
        {
            _usersRepository = postRepository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<UserDto>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetById(request.UserId);
            //var following = user.Subscriptions.Select(s => s.To);
            var following = _subscriptionRepository.Find(s => s.FromId == request.UserId).Select(u => u.ToId);
            var subs = _usersRepository.Find(u => following.Contains(u.Id));

            var result = _mapper.Map<ICollection<UserDto>>(subs);

            return result;
        }
    }
}
