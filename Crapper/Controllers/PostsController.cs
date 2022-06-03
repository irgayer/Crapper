using Crapper.DTOs.Post;
using Crapper.Features.PostsFeatures.Commands.AddPost;
using Crapper.Features.PostsFeatures.Commands.DeletePost;
using Crapper.Features.PostsFeatures.Queries.GetAllPosts;
using Crapper.Features.PostsFeatures.Queries.GetPostById;
using Crapper.Features.PostsFeatures.Queries.GetPostsByFilter;
using Crapper.Features.UserFeatures.Queries.GetUserById;
using Crapper.Features.UserFeatures.Queries.HasAccessToPost;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace Crapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : ControllerBase
    {     
        private readonly ISender _mediator;

        public PostsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PostDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(PostCreateDto req)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var post = await _mediator.Send(new AddPostCommand(req, id));
            if (post == null)
                return BadRequest();

            return Ok(post);
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ICollection<PostDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            var posts = await _mediator.Send(new GetAllPostsQuery());

            return Ok(posts);
        }

        [HttpGet("my")]
        [ProducesResponseType(typeof(ICollection<PostDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPostsByIdentity()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var posts = await _mediator.Send(new GetPostsByFilterQuery(x => x.Author.Id == id));
            return Ok(posts);
        }

        [HttpGet("user/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ICollection<PostDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUserAsync(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            
            if (user == null)
                return NotFound();

            var posts = await _mediator.Send(new GetPostsByFilterQuery(x => x.Author.Id == id));
            return Ok(posts);
        }

        [HttpDelete("{id}")]
        
        [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            // todo: move to filter
            var post = await _mediator.Send(new GetPostByIdQuery(id));
            if (post == null)
                return NotFound();

            // todo: move to middleware
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var hasAccess = await _mediator.Send(new HasAccessToPostQuery(userId, id));

            if (!hasAccess)
                return BadRequest();

            await _mediator.Send(new DeletePostCommand(id));
            return Ok(post);         
        }
    }
}
