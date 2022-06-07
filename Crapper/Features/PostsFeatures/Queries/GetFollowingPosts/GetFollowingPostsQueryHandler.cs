using AutoMapper;

using Crapper.DTOs.Post;
using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.PostsFeatures.Queries.GetFollowingPosts
{
    public class GetFollowingPostsQueryHandler : IRequestHandler<GetFollowingPostsQuery, ICollection<PostDto>>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IMapper _mapper;

        public GetFollowingPostsQueryHandler(IRepository<Post> repository, IMapper mapper, IRepository<Subscription> subscriptionRepository)
        {
            _postRepository = repository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public Task<ICollection<PostDto>> Handle(GetFollowingPostsQuery request, CancellationToken cancellationToken)
        {
            var following = _subscriptionRepository.Find(s => s.FromId == request.UserId).Select(u => u.ToId);
            var posts = _postRepository.Find(p => following.Contains(p.AuthorId));

            var result = _mapper.Map<ICollection<PostDto>>(posts);

            return Task.FromResult(result);
        }
    }
}
