using AutoMapper;

using Crapper.DTOs.Post;
using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.PostsFeatures.Queries.GetPostById
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IMapper _mapper;
        public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(request.Id);

            var result = _mapper.Map<PostDto>(post);

            return result;
        }
    }
}
