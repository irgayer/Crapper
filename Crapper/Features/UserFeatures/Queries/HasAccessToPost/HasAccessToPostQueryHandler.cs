using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.UserFeatures.Queries.HasAccessToPost
{
    public class HasAccessToPostQueryHandler : IRequestHandler<HasAccessToPostQuery, bool>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;

        public HasAccessToPostQueryHandler(IRepository<Post> postRepository, IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(HasAccessToPostQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId);
            var post = await _postRepository.GetById(request.PostId);

            if (user?.Id == post?.AuthorId)
                return true;

            return false;
        }
    }
}
