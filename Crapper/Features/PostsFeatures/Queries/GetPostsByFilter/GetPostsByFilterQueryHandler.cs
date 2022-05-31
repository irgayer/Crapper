using AutoMapper;

using Crapper.DTOs.Post;
using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.PostsFeatures.Queries.GetPostsByFilter
{
    public class GetPostsByFilterQueryHandler : IRequestHandler<GetPostsByFilterQuery, ICollection<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Post> _postRepository;

        public GetPostsByFilterQueryHandler(IMapper mapper, IRepository<Post> postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public Task<ICollection<PostDto>> Handle(GetPostsByFilterQuery request, CancellationToken cancellationToken)
        {
            var posts = _postRepository.Find(request.Predicate);
            var result = _mapper.Map<ICollection<PostDto>>(posts);

            return Task.FromResult(result);
        }
    }
}
