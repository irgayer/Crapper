using MediatR;

namespace Crapper.Features.LikesFeatures.Commands.ToggleLike
{
    public class ToggleLikeCommand : IRequest
    {
        public int UserId { get; set; }
        public int PostId { get; set; }

        public ToggleLikeCommand(int userId, int postId)
        {
            UserId = userId;
            PostId = postId;
        }
    }
}
