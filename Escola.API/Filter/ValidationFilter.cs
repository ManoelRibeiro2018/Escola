using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Escola.API.Filter
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var menssage = context.ModelState.SelectMany(ms => ms.Value.Errors).Select(err => err.ErrorMessage);
                context.Result = new BadRequestObjectResult(menssage);
            }
        }
    }
}
