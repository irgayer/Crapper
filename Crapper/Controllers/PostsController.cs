using AutoMapper;
using Crapper.DTOs.Post;
using Crapper.Interfaces;
using Crapper.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public PostsController(IRepository<Post> postRepository, IMapper mapper, IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(PostCreateDto req)
        {
            var post = _mapper.Map<Post>(req);
            var user = _userRepository.Find(user => user.Username == User.Identity.Name).SingleOrDefault();

            if (user == null)
                return BadRequest();

            post.AuthorId = user.Id;
            await _postRepository.Add(post);
            await _postRepository.Save();

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<PostDto>> GetAll()
        {
            var posts = _postRepository.GetAll();
            var res = _mapper.Map<IEnumerable<PostDto>>(posts.AsEnumerable());

            return Ok(res);
        }

        //todo: rewrite boilerplate, use GetByUser(id)
        [HttpGet("my")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPostsByIdentity()
        {
            var user = _userRepository.Find(user => user.Username == User.Identity.Name).SingleOrDefault();
            //var posts = user.Posts.AsEnumerable();
            var posts = _postRepository.Find(x => x.AuthorId == user.Id).AsEnumerable();
            var res = _mapper.Map<IEnumerable<PostDto>>(posts);

            return Ok(res);
        }

        [HttpGet("user/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUserAsync(int id)
        {
            var user = await _userRepository.GetById(id);
            
            if (user == null)
                return NotFound();

            var posts = _postRepository.Find(post => post.AuthorId == user.Id);
            var res = _mapper.Map<IEnumerable<PostDto>>(posts.AsEnumerable());

            return Ok(res);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = User.Identity.Name;
            var post = _postRepository.Find(post => post.Id == id).SingleOrDefault();

            if (post == null)
                return NotFound();
            if (user != post.Author.Username)
                return BadRequest();

            _postRepository.Delete(post);
            await _postRepository.Save();

            return Ok();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id)
        {
            var user = User.Identity.Name;
            var post = _postRepository.Find(post => post.Id == id).SingleOrDefault();

            if (post == null)
                return NotFound();
            if (user != post.Author.Username)
                return BadRequest();

            //todo: implement
            return Ok();
        }

    }
}
