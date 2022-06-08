using Crapper.Interfaces;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.LikesFeatures.Commands.ToggleLike
{
    public class ToggleLikeCommandHandler : IRequestHandler<ToggleLikeCommand>
    {
        private readonly IRepository<Like> _likesRepository;

        public ToggleLikeCommandHandler(IRepository<Like> likesRepository)
        {
            _likesRepository = likesRepository;
        }

        public async Task<Unit> Handle(ToggleLikeCommand request, CancellationToken cancellationToken)
        {
            var existing = _likesRepository
                .Find(l => l.UserId == request.UserId && l.PostId == request.PostId)
                .SingleOrDefault();

            if (existing == null)
            {
                var like = new Like { UserId = request.UserId, PostId = request.PostId };
                await _likesRepository.Add(like);
            }
            else
            {
                _likesRepository.Delete(existing);
            }

            await _likesRepository.Save();

            return Unit.Value;
        }
    }
}
