using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PushNotificationService.WebApi.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class ValidationFilter : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.ValidationState == ModelValidationState.Invalid)
            context.Result = new BadRequestObjectResult("Validation Failed!");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}