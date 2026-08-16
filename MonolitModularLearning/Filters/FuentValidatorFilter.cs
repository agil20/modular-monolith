using Microsoft.AspNetCore.Mvc.Filters;

namespace MonolitModularLearning.Filters
{
    public class FuentValidatorFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var arg in context.ActionArguments.Values)
            {
                Console.WriteLine(arg);
            }

         await    next();
        }
    }
}
