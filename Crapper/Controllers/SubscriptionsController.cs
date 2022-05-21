using Crapper.Interfaces;
using Crapper.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Subscription> _subscriptionRepository;

        public SubscriptionsController(IRepository<User> userRepository, IRepository<Subscription> subscriptionRepository)
        {
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        //todo: delete boilerplate
        [HttpPost("user/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Subscribe(int id)
        {
            var toUser = _userRepository.Find(user => user.Id == id).SingleOrDefault();
            var fromUser = _userRepository.Find(user => user.Username == User.Identity.Name).SingleOrDefault();

            if (toUser == null)
                return NotFound();

            if (toUser.Id == fromUser.Id)
                return BadRequest();

            var subscription = _subscriptionRepository
                .Find(s => s.FromId == fromUser.Id && s.ToId == toUser.Id).SingleOrDefault();

            if (subscription != null) 
                return BadRequest();

            subscription = new Subscription { FromId = fromUser.Id, ToId = toUser.Id};
            _subscriptionRepository.Add(subscription);
            _subscriptionRepository.Save();

            return Ok();
        }

        [HttpDelete("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Unsubscribe(int id)
        {
            var toUser = _userRepository.Find(user => user.Id == id).SingleOrDefault();
            var fromUser = _userRepository.Find(user => user.Username == User.Identity.Name).SingleOrDefault();

            if (toUser == null)
                return NotFound();

            if (toUser.Id == fromUser.Id)
                return BadRequest();

            var subscription = _subscriptionRepository
                .Find(s => s.FromId == fromUser.Id && s.ToId == toUser.Id).SingleOrDefault();

            if (subscription == null)
                return BadRequest();

            _subscriptionRepository.Delete(subscription);
            _subscriptionRepository.Save();

            return Ok();
        }

    } 
}
