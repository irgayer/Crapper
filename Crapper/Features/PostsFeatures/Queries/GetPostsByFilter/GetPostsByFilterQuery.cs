using Crapper.DTOs.Post;
using Crapper.Models;

using MediatR;

using System.Linq.Expressions;

namespace Crapper.Features.PostsFeatures.Queries.GetPostsByFilter
{
    public class GetPostsByFilterQuery : IRequest<ICollection<PostDto>>
    {
        public Expression<Func<Post, bool>> Predicate { get; set; }

        public GetPostsByFilterQuery(Expression<Func<Post, bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
