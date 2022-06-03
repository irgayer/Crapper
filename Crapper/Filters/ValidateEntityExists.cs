using Crapper.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Crapper.Filters
{
    public class ValidateEntityExists<T> : IAsyncActionFilter where T : class
    {
        private readonly IRepository<T> _repository;

        public ValidateEntityExists(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionArguments.ContainsKey("id"))
            {
                context.Result = new BadRequestResult();
            }

            int id = (int)context.ActionArguments["id"];
            var entity = await _repository.GetById(id);

            if (entity == null)
                context.Result = new NotFoundResult();
            else
                context.HttpContext.Items.Add("entity", entity);

            if (context.Result == null)
                await next();
        }
    }
}
