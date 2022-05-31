using AutoMapper;

using Crapper.DTOs.Post;
using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.PostsFeatures.Commands.AddPost
{
    public class AddPostCommandHandler : IRequestHandler<AddPostCommand, PostDto>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public AddPostCommandHandler(IRepository<Post> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request.Post);
            post.AuthorId = request.AuthorId;

            await _postRepository.Add(post);
            await _postRepository.Save();

            return _mapper.Map<PostDto>(post);
        }
    }
}
