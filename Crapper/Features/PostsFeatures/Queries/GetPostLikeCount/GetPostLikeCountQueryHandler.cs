using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.PostsFeatures.Queries.GetPostLikeCount
{
    public class GetPostLikeCountQueryHandler : IRequestHandler<GetPostLikeCountQuery, int>
    {
        private readonly IRepository<Post> _postsRepository;

        public GetPostLikeCountQueryHandler(IRepository<Post> postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<int> Handle(GetPostLikeCountQuery request, CancellationToken cancellationToken)
        {
            var post = await _postsRepository.GetById(request.Id);
            int likes = post.LikesCount;

            return likes;
        }
    }
}
