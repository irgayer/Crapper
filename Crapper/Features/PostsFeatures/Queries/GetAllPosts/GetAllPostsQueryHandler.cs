using AutoMapper;

using Crapper.DTOs.Post;
using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.PostsFeatures.Queries.GetAllPosts
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, ICollection<PostDto>>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IRepository<Post> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public Task<ICollection<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = _postRepository.GetAll();
            var result = _mapper.Map<ICollection<PostDto>>(posts);
            
            return Task.FromResult(result);
        }
    }
}
