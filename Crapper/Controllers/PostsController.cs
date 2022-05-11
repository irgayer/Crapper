using AutoMapper;
using Crapper.DTOs.Post;
using Crapper.Interfaces;
using Crapper.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create(PostCreateDto req)
        {
            var post = _mapper.Map<Post>(req);
            var user = _userRepository.Find(user => user.Username == User.Identity.Name).SingleOrDefault();

            if (user == null)
                return BadRequest();

            post.UserId = user.Id;
            _postRepository.Add(post);
            _postRepository.Save();

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

    }
}
