using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EShop.Web.Filters
{
    public class HasIdParameterAttribute : Attribute, IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["id"] is null || long.Parse(context.ActionArguments["id"]?.ToString() ?? "0") < 1)
            {
                context.Result = new BadRequestResult();
            }
            else
            {
                await next();
            }
        }
    }
}
