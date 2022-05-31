using MediatR;

namespace Crapper.Features.PostsFeatures.Commands.DeletePost
{
    public class DeletePostCommand : IRequest
    {
        public int Id { get; set; }

        public DeletePostCommand(int id)
        {
            Id = id;
        }
    }
}
