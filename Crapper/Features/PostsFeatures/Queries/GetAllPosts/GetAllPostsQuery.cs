using Crapper.DTOs.Post;

using MediatR;

namespace Crapper.Features.PostsFeatures.Queries.GetAllPosts
{
    public class GetAllPostsQuery : IRequest<ICollection<PostDto>>
    {
        
    }
}
