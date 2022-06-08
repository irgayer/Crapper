using MediatR;

namespace Crapper.Features.PostsFeatures.Queries.GetPostLikeCount
{
    public class GetPostLikeCountQuery : IRequest<int>
    {
        public int Id { get; set; }

        public GetPostLikeCountQuery(int id)
        {
            Id = id;
        }
    }
}
