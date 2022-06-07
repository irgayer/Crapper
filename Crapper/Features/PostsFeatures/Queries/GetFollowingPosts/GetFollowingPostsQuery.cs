using Crapper.DTOs.Post;

using MediatR;

namespace Crapper.Features.PostsFeatures.Queries.GetFollowingPosts
{
    public class GetFollowingPostsQuery : IRequest<ICollection<PostDto>>
    {
        public int UserId { get; set; }

        public GetFollowingPostsQuery(int userId)
        {
            UserId = userId;
        }
    }
}
