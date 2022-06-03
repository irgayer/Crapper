using Crapper.Interfaces;
using Crapper.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Security.Claims;

namespace Crapper.Filters
{
    public class UserPostAccessFilter : IAsyncActionFilter
    {
        private readonly IRepository<Post> _repository;

        public UserPostAccessFilter(IRepository<Post> repository)
        {
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int userId = int.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            // todo: unit test! 
            // warning: not safe
            Post? post = context.HttpContext.Items["entity"] as Post;
            if (userId != post?.AuthorId)
                context.Result = new BadRequestResult();

            if (context.Result == null)
                await next();
        }
    }
}
