using Crapper.DTOs.Post;

using MediatR;

namespace Crapper.Features.PostsFeatures.Queries.GetPostById
{
    public class GetPostByIdQuery : IRequest<PostDto>
    {
        public int Id { get; set; }
        
        public GetPostByIdQuery(int id)
        {
            Id = id;
        }
    }
}
