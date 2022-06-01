using MediatR;

namespace Crapper.Features.UserFeatures.Queries.HasAccessToPost
{
    public class HasAccessToPostQuery : IRequest<bool>
    {
        public int UserId { get; set; }
        public int PostId { get; set; }

        public HasAccessToPostQuery(int userId, int postId)
        {
            UserId = userId;
            PostId = postId;
        }
    }
}
