using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.PostsFeatures.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IRepository<Post> _postRepository;

        public DeletePostCommandHandler(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(request.Id);
            
            _postRepository.Delete(post);
            await _postRepository.Save();

            return Unit.Value;
        }
    }
}
