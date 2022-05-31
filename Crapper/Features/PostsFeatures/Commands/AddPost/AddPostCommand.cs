using Crapper.DTOs.Post;
using Crapper.Models;

using MediatR;

namespace Crapper.Features.PostsFeatures.Commands.AddPost
{
    public class AddPostCommand : IRequest<PostDto>
    {
        public PostCreateDto Post { get; set; }
        public int AuthorId { get; set; }

        public AddPostCommand(PostCreateDto post, int authorId)
        {
            Post = post;
            AuthorId = authorId;
        }
    }
}
