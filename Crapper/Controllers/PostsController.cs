using Crapper.DTOs.Post;
using Crapper.Features.LikesFeatures.Commands.ToggleLike;
using Crapper.Features.PostsFeatures.Commands.AddPost;
using Crapper.Features.PostsFeatures.Commands.DeletePost;
using Crapper.Features.PostsFeatures.Queries.GetAllPosts;
using Crapper.Features.PostsFeatures.Queries.GetFollowingPosts;
using Crapper.Features.PostsFeatures.Queries.GetPostLikeCount;
using Crapper.Features.PostsFeatures.Queries.GetPostsByFilter;
using Crapper.Filters;
using Crapper.Models;

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

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ICollection<PostDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            var posts = await _mediator.Send(new GetAllPostsQuery());

            return Ok(posts);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PostDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(PostCreateDto req)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var post = await _mediator.Send(new AddPostCommand(req, id));            

            return Ok(post);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExists<Post>))]
        [ServiceFilter(typeof(UserPostAccessFilter))]
        [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePostCommand(id));
            return Ok();
        }

        [HttpPost("{id}/like")]
        [ServiceFilter(typeof(ValidateEntityExists<Post>))]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Like(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _mediator.Send(new ToggleLikeCommand(userId, id));
            int likes = await _mediator.Send(new GetPostLikeCountQuery(id));

            return Ok(likes);
        }

        [HttpGet("subs")]
        [ProducesResponseType(typeof(ICollection<PostDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFollowingPosts()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var posts = await _mediator.Send(new GetFollowingPostsQuery(id));
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
        [ServiceFilter(typeof(ValidateEntityExists<User>))]
        [ProducesResponseType(typeof(ICollection<PostDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUserAsync(int id)
        {
            var posts = await _mediator.Send(new GetPostsByFilterQuery(x => x.Author.Id == id));
            return Ok(posts);
        }
    }
}
